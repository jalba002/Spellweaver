using Spellweaver.Data;
using Spellweaver.Managers;
using System.Collections.ObjectModel;

namespace Spellweaver.ViewModel
{
    public class SpellEditorViewModel : ViewModelBase
    {
        private DNDDatabase _dbProvider;
        public SpellEditorViewModel(DNDDatabase dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public SpellItemViewModel? SelectedSpell
        {
            get
            {
                return SpellManager.CurrentSpell;
            }
            set
            {
                SpellManager.CurrentSpell = value;
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
                var schools = await _dbProvider.GetSchoolsAsync();
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
                var castingTimes = await _dbProvider.GetCastingTimesAsync();
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
                var levels = await _dbProvider.GetLevelsAsync();
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