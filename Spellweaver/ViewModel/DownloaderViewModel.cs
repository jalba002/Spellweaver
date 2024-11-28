using Microsoft.Extensions.Logging;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Interfaces;
using Spellweaver.Managers;
using Spellweaver.Other;
using Spellweaver.Providers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace Spellweaver.ViewModel
{
    public class DownloaderViewModel : ViewModelBase
    {
        private DNDDatabase _dbProvider;
        public DownloaderViewModel(DNDDatabase db)
        {
            _dbProvider = db;

            AddSpellCommand = new DelegateCommand(AddSpell, CanAdd);
            DownloadSpellsCommand = new AsyncCommand(DownloadSpells, CanSearch);

            // Filters
            FilterList = new Dictionary<string, Func<string?>>()
            {
                { "name", () => NameFilter },
                { "spell_lists", () => ClassFilter },
                { "spell_level", () => LevelFilter },
                { "school", () => SchoolFilter },
                //{ "document__title", () => SourceFilter },
                //{ "requires_verbal_components", () => SourceFilter },
                //{ "requires_somatic_components", () => SourceFilter },
                //{ "requires_material_components", () => SourceFilter },
                //{ "can_be_cast_as_ritual", () => SourceFilter },
            };

            // Sorting
            SortingList = new ObservableCollection<SortingEntry>()
            {
                new SortingEntry("Name Asc.", new SortDescription("Name", ListSortDirection.Ascending)),
                new SortingEntry("Name Desc.", new SortDescription("Name", ListSortDirection.Descending)),
                new SortingEntry("Level Asc.", new SortDescription("Level", ListSortDirection.Ascending)),
                new SortingEntry("Level Desc.", new SortDescription("Level", ListSortDirection.Descending)),
                new SortingEntry("School Asc.", new SortDescription("School", ListSortDirection.Ascending)),
                new SortingEntry("School Desc.", new SortDescription("School", ListSortDirection.Descending)),
            };

            // Filters
            _spellsView = CollectionViewSource.GetDefaultView(DownloadedSpells);

            // Default
            SortingMethod = SortingList[0];
        }

        #region Commands
        public DelegateCommand AddSpellCommand { get; }
        public IAsyncCommand DownloadSpellsCommand { get; }
        private bool CanSearch() => !IsBusy;
        private bool CanAdd(object? parameter) => SelectedSpell is not null;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
                DownloadSpellsCommand.RaiseCanExecuteChanged();
            }
        }

        #region Sorting Properties
        public ObservableCollection<SortingEntry> SortingList { get; private set; }

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

        #region Parameters

        private Dictionary<string, Func<string?>> FilterList = new Dictionary<string, Func<string?>>();
        // All parameters are listed here for search. Let's go
        private string? _nameFilter;
        public string? NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
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
                RaisePropertyChanged();
            }
        }

        #endregion

        private void AddSpell(object? parameter)
        {
            var objectList = ((IEnumerable<object>)parameter).Cast<SpellItemViewModel>().ToList();
            string msg = "Added: ";
            foreach (SpellItemViewModel item in objectList)
            {
                msg += item.Name + ", ";
            }
            msg = msg.Remove(msg.Length - 2, 2);
            //_logger.Log(LogLevel.Information, msg);
            //StatusBarManager.Instance.SetInfo(msg);
            SpellManager.AddToSpellList(objectList);
        }

        private async Task DownloadSpells()
        {
            // Add spells to the spell list.
            var parameterList = new Dictionary<string, string>();
            try
            {
                IsBusy = true;
                DownloadedSpells.Clear();
                // Build parameters
                foreach (var item in FilterList)
                {
                    var valueFiltered = item.Value?.Invoke();
                    if (!string.IsNullOrEmpty(valueFiltered))
                    {
                        parameterList.Add(item.Key, valueFiltered.ToLower());
                    }
                }
                // Send parameters
                var result = await ((OnlineDatabaseProvider)_dbProvider).GetSpellsWithParameters(parameterList);
                //var result = await ((OnlineDatabaseProvider)_dbProvider).GetAllSpellsThatMatch(MatchText);
                foreach (Spell item in result)
                {
                    DownloadedSpells.Add(new SpellItemViewModel(item));
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"Error when downloading spells:\n{ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Downloaded Spells Collection
        private ICollectionView _spellsView;
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

        public ObservableCollection<SpellItemViewModel> DownloadedSpells { get; } = new();
        #endregion

        private SpellItemViewModel? _selectedSpell;
        public SpellItemViewModel? SelectedSpell
        {
            get
            {
                return _selectedSpell;
            }
            set
            {
                _selectedSpell = value;
                RaisePropertyChanged();
                AddSpellCommand.RaiseCanExecuteChanged();
            }
        }
    }
}