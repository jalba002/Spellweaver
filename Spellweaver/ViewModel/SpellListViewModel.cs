using Serilog;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Interfaces;
using Spellweaver.Managers;
using Spellweaver.Providers;
using Spellweaver.Services.Backend;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using static Spellweaver.Services.Backend.ExporterFactory;

namespace Spellweaver.ViewModel
{
    public class SpellListViewModel : ViewModelBase
    {
        private DNDDatabase _dbProvider;
        private IErrorHandler _errorHandler;
        private ExporterFactory _exporter;
        public SpellListViewModel(
            DNDDatabase dB,
            IErrorHandler errorHandler,
            ExporterFactory exporter)
        {
            _dbProvider = dB;
            _errorHandler = errorHandler;
            _exporter = exporter;

            // We must register all commands here as they are readonly.
            AddCommand = new DelegateCommand(Add);
            DuplicateSpellCommand = new DelegateCommand(DuplicateSpell);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            RemoveSingleSpellCommand = new DelegateCommand(RemoveSingle);

            // Shortcuts
            LoadSpellToCardView = new DelegateCommand(LoadCardView);
            LoadSpellToEditView = new DelegateCommand(LoadEditView);

            // Download spells
            DownloadSpellsCommand = new AsyncCommand(DownloadSpells, CanExecuteCommand);
            DownloadAllSpellsCommand = new AsyncCommand(DownloadAllSpells, CanExecuteCommand);

            FilterList = new Dictionary<string, Func<string?>>()
            {
                { "Name", () => NameFilter },
                { "Classes", () => ClassFilter },
                { "Level", () => LevelFilter },
                { "Description", () => DescriptionFilter },
                { "Source", () => SourceFilter }
            };

            SortingList = new ObservableCollection<SortingEntry>()
            {
                new SortingEntry("Name Asc.", new SortDescription("Name", ListSortDirection.Ascending)),
                new SortingEntry("Name Desc.", new SortDescription("Name", ListSortDirection.Descending)),
                new SortingEntry("School Asc.", new SortDescription("School", ListSortDirection.Ascending)),
                new SortingEntry("School Desc.", new SortDescription("School", ListSortDirection.Descending)),
            };

            // Load view!
            _spellsView = CollectionViewSource.GetDefaultView(SpellManager.SpellList);
            _spellsView.Filter = FilterSpell;

            SortingMethod = SortingList[0];
        }

        #region Collections
        // This collection is to only visualize.
        // The realone should be at SpellManager
        private ICollectionView _spellsView;
        public ObservableCollection<MenuItemViewModel> ExportMenuItems { get; set; }
        public ObservableCollection<MenuItemViewModel> ImportMenuItems { get; set; }
        public ObservableCollection<SpellItemViewModel> Spells { get { return SpellManager.SpellList; } }

        public ICollectionView SpellsView
        {
            get
            {
                return _spellsView;
            }
            set
            {
                _spellsView = value;
                RaisePropertyChanged();
            }
        }

        #region Filter Properties

        private Dictionary<string, Func<string?>> FilterList = new Dictionary<string, Func<string?>>();

        private string? _nameFilter;
        public string? NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private string? _levelFilter;
        public string? LevelFilter
        {
            get { return _levelFilter; }
            set
            {
                _levelFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private string? _classFilter;
        public string? ClassFilter
        {
            get { return _classFilter; }
            set
            {
                _classFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private string? _descriptionFilter;
        public string? DescriptionFilter
        {
            get { return _descriptionFilter; }
            set
            {
                _descriptionFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private string? _sourceFilter;
        public string? SourceFilter
        {
            get { return _sourceFilter; }
            set
            {
                _sourceFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private string? _schoolFilter;
        public string? SchoolFilter
        {
            get { return _schoolFilter; }
            set
            {
                _schoolFilter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Sorting Properties
        public ObservableCollection<SortingEntry> SortingList { get; private set; }

        [System.Serializable]
        public class SortingEntry
        {
            public SortingEntry(string name, SortDescription sortingMethod)
            {
                Name = name;
                SortingMethod = sortingMethod;
            }

            public string Name { get; set; }
            public SortDescription SortingMethod { get; set; }
        }

        private SortingEntry _selectedSortingMethod;
        public SortingEntry SortingMethod
        {
            get => _selectedSortingMethod;
            set
            {
                _selectedSortingMethod = value;
                ApplySort(value);
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        public void ApplySort(SortingEntry sortingEntry)
        {
            if (sortingEntry == null) return;
            _spellsView.SortDescriptions.Clear();
            _spellsView.SortDescriptions.Add(sortingEntry.SortingMethod);
        }

        #endregion

        private bool FilterSpell(object? spell)
        {
            SpellItemViewModel modelSpell = (SpellItemViewModel)spell;

            bool contains = true;
            foreach (var entry in FilterList)
            {
                string entryValue = entry.Value?.Invoke();
                if (!string.IsNullOrEmpty(entryValue))
                {
                    // Analyzing entry.Key!
                    var propertyInfo = typeof(SpellItemViewModel).GetProperty(entry.Key);
                    contains = ((string)propertyInfo.GetValue(modelSpell)).Contains(entryValue, StringComparison.CurrentCultureIgnoreCase);
                    if (!contains) return false;
                }
            }
            return contains;
        }

        #endregion

        #region Properties

        public SpellItemViewModel? SelectedSpell
        {
            get => SpellManager.CurrentSpell;
            set
            {
                SpellManager.CurrentSpell = value;
                RaisePropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                _isBusy = value;
                RaisePropertyChanged();
                DownloadSpellsCommand.RaiseCanExecuteChanged();
                DownloadAllSpellsCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        public async override Task LoadAsync()
        {
            // Load to ContextMenu all available options, and on click, have code behind for it?
            LoadContextMenuForSpells();
        }

        // Load all buttons!
        private void LoadContextMenuForSpells()
        {
            ExportMenuItems = new ObservableCollection<MenuItemViewModel>();
            ImportMenuItems = new ObservableCollection<MenuItemViewModel>();

            var listOfEntries = _exporter.GetAvailableExporters();
            foreach (var item in listOfEntries)
            {
                ExportMenuItems.Add(new MenuItemViewModel()
                {
                    Command = new CommandViewModel(() => { ExportSpells(item); }),
                    Header = item.ToString()
                });
                ImportMenuItems.Add(new MenuItemViewModel()
                {
                    Command = new CommandViewModel(() => { ImportSpells(item); }),
                    Header = item.ToString()
                });
            }
        }

        private void ExportSpells(ExportType exportType)
        {
            // This will handle ALLLLLLL of the exportation process.
            try
            {
                IsBusy = true;
                List<Spell> spells = new List<Spell>();
                foreach (SpellItemViewModel spell in Spells)
                {
                    spells.Add(spell.GetModel);
                }
                // Send over to get the string from factory
                var exportString = _exporter.Export(spells, exportType);
                // Decide the path of exportation
                SpellExportWindowManager.Export(exportString);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
            finally
            {
                IsBusy = false;
            }
            // Call the window for opening stuff?
        }

        private void ImportSpells(ExportType exportType)
        {
            // This will handle ALLLLLLL of the exportation process.
            try
            {
                IsBusy = true;

                // Send over to get the string from factory
                // Decide the path of exportation
                string? importString = SpellExportWindowManager.Import();
                if (importString == null) return;

                List<Spell> SPELLS = _exporter.Import(importString, exportType);
                if (SPELLS == null || SPELLS.Count <= 0)
                {
                    Log.Warning($"Exiting import method due to spells being empty or null");
                    return;
                }
                foreach (var item in SPELLS)
                {
                    ImportSpell(item);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
            finally
            {
                IsBusy = false;
            }
            // Call the window for opening stuff?
        }

        #region Commands
        public DelegateCommand AddCommand { get; }

        public DelegateCommand DuplicateSpellCommand { get; }
        public DelegateCommand RemoveSingleSpellCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ShowSpellDebugCommand { get; }

        // Shortcuts Command
        public DelegateCommand LoadSpellToCardView { get; }
        public DelegateCommand LoadSpellToEditView { get; }

        public IAsyncCommand DownloadSpellsCommand { get; }
        public IAsyncCommand DownloadAllSpellsCommand { get; }
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        //public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        private void Add(object? parameter)
        {
            var spell = new Spell
            {
                Name = "Default Spell",
                Level = "0",
                CastingTime = "1 action",
                Classes = "Wizard",
                Description = "Default Spell for Testing",
                School = "Evocation",
                Range = "60ft",
                Source = "Spellweaver"
            };
            var viewModel = new SpellItemViewModel(spell);
            SpellManager.AddToSpellList(viewModel);
            SelectedSpell = viewModel;
        }

        private void DuplicateSpell(object? parameter)
        {
            if (parameter == null) return;
            //if (SelectedSpell != null)
            //{
            //    SpellManager.AddToSpellListAfterSelectedSpell(SelectedSpell.Clone() as SpellItemViewModel);
            //}
            SpellItemViewModel objectList = (SpellItemViewModel)parameter;
            SpellManager.AddToSpellListAfterSelectedSpell(objectList.Clone() as SpellItemViewModel);
        }

        private bool CanRemove(object? parameter) => SelectedSpell is not null;

        private void RemoveSingle(object? parameter)
        {
            if (parameter == null) return;
            var objectList = parameter as SpellItemViewModel;
            if (objectList != null)
            {
                MessageBoxResult m = MessageBox.Show($"Are you sure you want to remove <{objectList.Name}> from the list?", "Removing Spell(s)", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    SpellManager.RemoveSpellFromList(objectList);
                    SelectedSpell = null;
                }
            }
        }

        private void Remove(object? parameter)
        {
            if (parameter == null) return;
            var objectList = ((IEnumerable<object>)parameter).Cast<SpellItemViewModel>().ToList();
            if (objectList != null && objectList.Count > 0)
            {
                var allSpellsNames = String.Join("\n", objectList.Select(x => x.Name));
                MessageBoxResult m = MessageBox.Show($"Are you sure you want to remove:\n\n{allSpellsNames}\n\n from the list?", "Removing Spell(s)", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    //Spells.Remove(SelectedSpell);
                    SpellManager.RemoveSpellFromList(objectList);
                    SelectedSpell = null;
                }
            }
            else
            {
                // Logger?
                // Error Logger
            }
        }

        private void LoadCardView(object? parameter)
        {
            // The object is now selected
            if (parameter == null) return;

            SpellItemViewModel spellModel = (SpellItemViewModel)parameter;

            SelectedSpell = spellModel;
            MainViewModel.Instance.LoadSpellCardViewCommand.Execute(null);
        }

        private void LoadEditView(object? parameter)
        {
            // The object is now selected
            if (parameter == null) return;

            SpellItemViewModel spellModel = (SpellItemViewModel)parameter;

            SelectedSpell = spellModel;
            MainViewModel.Instance.LoadSpellEditorCommand.Execute(null);
        }

        //// This is binded to the command to open the window and then importSpells from a file.
        //// It import any amount, as it is always exported as an array.
        //private void ImportSpells(object? parameter)
        //{
        //    var importResult = SpellExportWindowManager.ImportSpells<Spell>();
        //    Log.Information($"Adding {importResult.ToArray().Length} new spells from import mode");
        //    foreach (var item in importResult)
        //    {
        //        ImportSpell(item);
        //    }
        //}

        private void ImportSpell(Spell? spell)
        {
            if (spell is not null)
            {
                //Spells.Add(new SpellItemViewModel(spell.TransformToInternalModel()));
                //Spells.Add(new SpellItemViewModel(spell));
                SpellManager.AddToSpellList(new SpellItemViewModel(spell));
            }
        }

        private bool CanExecuteCommand()
        {
            return !IsBusy;
        }

        private async Task DownloadSpells()
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            try
            {
                IsBusy = true;
                var spellsResult = await _dbProvider.GetSpellsAsync();
                if (spellsResult.Count < 0)
                {
                    MessageBox.Show("No spells received when importing");
                    return;
                }
                UpdateSpellList(spellsResult);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DownloadAllSpells()
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            try
            {
                IsBusy = true;
                var spellsResult = await ((OnlineDatabaseProvider)_dbProvider).GetALLSpellsAsync();
                if (spellsResult.Count < 0)
                {

                    return;
                }
                UpdateSpellList(spellsResult);
            }
            finally { IsBusy = false; }
        }

        private void UpdateSpellList(List<Spell> spellList)
        {
            SelectedSpell = null;
            SpellManager.SetSpellList(spellList);
        }

        //private void ExportSpell(object? parameter)
        //{
        //    if (SelectedSpell == null) return;
        //    //SpellExportWindowManager.ExportSpells(new List<Spell>() { SelectedSpell.GetModel.TransformToInternalModel() });
        //}

        //private void ExportSpells(object? parameter)
        //{
        //    List<Spell> exportables = new List<Spell>();
        //    foreach (var item in Spells)
        //    {
        //        exportables.Add(item.GetModel.TransformToInternalModel());
        //    }
        //    //SpellExportWindowManager.ExportSpells(exportables);
        //}


        #endregion
    }
}