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
            DownloadSpellsCommand = new DelegateCommand(DownloadSpells, CanSearch);
        }

        #region Commands
        public DelegateCommand AddSpellCommand { get; }
        public DelegateCommand DownloadSpellsCommand { get; }
        private bool CanSearch(object? parameter) => !string.IsNullOrEmpty(MatchText);
        private bool CanAdd(object? parameter) => SelectedSpell is not null;

        private void AddSpell(object? parameter)
        {
            SpellManager.AddToSpellList(SelectedSpell);
        }

        private async void DownloadSpells(object? parameter)
        {
            // Add spells to the spell list.
            DownloadedSpells.Clear();
            var result = await ((OnlineDatabaseProvider)_dbProvider).GetAllSpellsThatMatch(MatchText);
            foreach (Spell item in result)
            {
                DownloadedSpells.Add(new SpellItemViewModel(item));
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