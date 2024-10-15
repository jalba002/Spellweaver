using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Providers;
using System.Collections.ObjectModel;

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
        }

        #region Commands
        public DelegateCommand AddSpellCommand { get; }
        public IAsyncCommand DownloadSpellsCommand { get; }
        private bool CanSearch() => !string.IsNullOrEmpty(MatchText) && !IsBusy;
        private bool CanAdd(object? parameter) => SelectedSpell is not null;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                DownloadSpellsCommand.RaiseCanExecuteChanged();
            }
        }

        private void AddSpell(object? parameter)
        {
            SpellManager.AddToSpellList(SelectedSpell);
        }

        private async Task DownloadSpells()
        {
            // Add spells to the spell list.
            try
            {
                IsBusy = true;
                DownloadedSpells.Clear();
                var result = await ((OnlineDatabaseProvider)_dbProvider).GetAllSpellsThatMatch(MatchText);
                foreach (Spell item in result)
                {
                    DownloadedSpells.Add(new SpellItemViewModel(item));
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Downloaded Spells Collection
        public ObservableCollection<SpellItemViewModel> DownloadedSpells { get; } = new();
        #endregion

        private string? _matchText;
        public string? MatchText
        {
            get
            {
                return _matchText;
            }
            set
            {
                _matchText = value;
                RaisePropertyChanged();
                DownloadSpellsCommand.RaiseCanExecuteChanged();
            }
        }

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