using Serilog;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Providers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;

namespace Spellweaver.ViewModel
{
    public class SpellListViewModel : ViewModelBase
    {
        private DNDDatabase _dbProvider;
        public SpellListViewModel(DNDDatabase dB)
        {
            _dbProvider = dB;

            // We must register all commands here as they are readonly.
            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            ShowSpellDebugCommand = new DelegateCommand(ShowDebugSpell);
            // Download spells
            DownloadSpellsCommand = new DelegateCommand(DownloadSpells);
            DownloadAllSpellsCommand = new DelegateCommand(DownloadAllSpells);
            // Export Spells Commands
            ExportSpellCommand = new DelegateCommand(ExportSpell);
            ExportSpellsCommand = new DelegateCommand(ExportSpells);
            // Import Spells Commands
            ImportSpellsCommand = new DelegateCommand(ImportSpells);

            _spellsView = CollectionViewSource.GetDefaultView(SpellManager.SpellList);
            _spellsView.Filter = x => FilterSpell(x, Filter);
        }

        #region Collections
        // This collection is to only visualize.
        // The realone should be at SpellManager
        private ICollectionView _spellsView;
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

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                _spellsView.Refresh();
                RaisePropertyChanged();
            }
        }

        private bool FilterSpell(object? spell, string filter)
        {
            if (String.IsNullOrEmpty(Filter)) return true;
            bool contains = false;
            SpellItemViewModel modelSpell = (SpellItemViewModel)spell;

            filter = filter.Replace(" ", "");

            contains = modelSpell.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.Description.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.Classes.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.LevelFormatted.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.ComponentsString.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.Source.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            modelSpell.School.Contains(filter, StringComparison.InvariantCultureIgnoreCase);

            return contains;
        }

        #endregion

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

        

        public async override Task LoadAsync()
        {

        }

        #region Commands
        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ShowSpellDebugCommand { get; }
        public DelegateCommand DownloadSpellsCommand { get; }
        public DelegateCommand DownloadAllSpellsCommand { get; }
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        //public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        private void AddToLists(SpellItemViewModel spell)
        {

        }

        private void Add(object? parameter)
        {
            var spell = new Spell { Name = "Default Spell", Level = "0", CastingTime = "1 action" };
            var viewModel = new SpellItemViewModel(spell);
            SpellManager.AddToSpellList(viewModel);
            SelectedSpell = viewModel;
        }

        private void Remove(object? parameter)
        {
            if (SelectedSpell is not null)
            {
                MessageBoxResult m = MessageBox.Show("Are you sure you want to remove " + SelectedSpell.Name + " from the list?", "Removing Spell", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    //Spells.Remove(SelectedSpell);
                    SpellManager.RemoveSpellFromList(SelectedSpell);
                    SelectedSpell = null;
                }
            }
        }
        // This is binded to the command to open the window and then importSpells from a file.
        // It import any amount, as it is always exported as an array.
        private void ImportSpells(object? parameter)
        {
            var importResult = SpellExportWindowManager.ImportSpells<Spell>();
            Log.Information($"Adding {importResult.ToArray().Length} new spells from import mode");
            foreach (var item in importResult)
            {
                ImportSpell(item);
            }
        }

        private void ImportSpell(Spell? spell)
        {
            if (spell is not null)
            {
                //Spells.Add(new SpellItemViewModel(spell.TransformToInternalModel()));
                //Spells.Add(new SpellItemViewModel(spell));
                SpellManager.AddToSpellList(new SpellItemViewModel(spell));
            }
        }

        private async void DownloadSpells(object? parameter)
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            var spellsResult = await _dbProvider.GetSpellsAsync();
            if (spellsResult.Count < 0)
            {
                MessageBox.Show("No spells received when importing");
                return;
            }
            UpdateSpellList(spellsResult);
        }

        private async void DownloadAllSpells(object? parameter)
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            var spellsResult = await ((OnlineDatabaseProvider)_dbProvider).GetALLSpellsAsync();
            if (spellsResult.Count < 0)
            {
                MessageBox.Show("No spells received when importing");
                return;
            }
            UpdateSpellList(spellsResult);
        }

        private void UpdateSpellList(List<Spell> spellList)
        {
            SelectedSpell = null;
            SpellManager.SetSpellList(spellList);
        }

        private void ExportSpell(object? parameter)
        {
            if (SelectedSpell == null) return;
            SpellExportWindowManager.ExportSpells(new List<Spell>() { SelectedSpell.GetModel.TransformToInternalModel() });
        }

        private void ExportSpells(object? parameter)
        {
            List<Spell> exportables = new List<Spell>();
            foreach (var item in Spells)
            {
                exportables.Add(item.GetModel.TransformToInternalModel());
            }
            SpellExportWindowManager.ExportSpells(exportables);
        }

        private bool CanRemove(object? parameter) => SelectedSpell is not null;
        private void ShowDebugSpell(object? parameter)
        {
            if (SelectedSpell is not null)
            {
                MessageBox.Show(JsonSerializer.Serialize(SelectedSpell.GetModel), "Debugging Spell");
            }
            else
            {
                MessageBox.Show("Select a spell first.", "SelectedSpell is null", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
    }
}