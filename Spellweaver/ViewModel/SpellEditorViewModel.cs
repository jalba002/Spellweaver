using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Model;
using System.Collections.ObjectModel;

namespace Spellweaver.ViewModel
{
    public class SpellEditorViewModel : ViewModelBase
    {
        private IDBProvider<DNDDatabase> _dbProvider;
        private SpellManager _spellManager;
        public SpellEditorViewModel(IDBProvider<DNDDatabase> dbProvider, SpellManager spellManager)
        {
            _spellManager = spellManager;
            _dbProvider = dbProvider;
        }

        public SpellItemViewModel? SelectedSpell
        {
            get
            {
                return _spellManager.CurrentSpell;
            }
            set
            {
                _spellManager.CurrentSpell = value;
                RaisePropertyChanged();
            }
        }

        #region Collections
        public ObservableCollection<SchoolItemViewModel> Schools { get; } = new();
        public ObservableCollection<CastingTimeItemViewModel> CastingTimes { get; } = new();
        public ObservableCollection<LevelItemViewModel> Levels { get; } = new();
        #endregion

        public bool CanBeOpened() => SelectedSpell != null;
        
        public async override Task LoadAsync()
        {
            if (!Schools.Any())
            {
                var schools = (await _dbProvider.GetAllAsync())?.Schools;
                if (schools is not null)
                {
                    foreach (var school in schools)
                    {
                        Schools.Add(new SchoolItemViewModel(school));
                    }
                }
            }
            if (!CastingTimes.Any())
            {
                var castingTimes = (await _dbProvider.GetAllAsync())?.CastingTimes;
                if (castingTimes is not null)
                {
                    foreach (var castingTime in castingTimes)
                    {
                        CastingTimes.Add(new CastingTimeItemViewModel(castingTime));
                    }
                }
            }
            if (!Levels.Any())
            {
                var levels = (await _dbProvider.GetAllAsync())?.Levels;
                if (levels is not null)
                {
                    foreach (var level in levels)
                    {
                        Levels.Add(new LevelItemViewModel(level));
                    }
                }
            }
        }
    }
}