using Spellweaver.Model;

namespace Spellweaver.ViewModel
{
    public class LevelItemViewModel : ViewModelBase<Level>
    {
        public LevelItemViewModel(Level model) : base(model)
        {
            
        }

        public string? LevelString
        {
            get => _model.LevelString;

            private set
            {
                _model.LevelString = value;
                RaisePropertyChanged();
            }
        }
        public int? LevelNumber
        {
            get => _model.LevelInt;

            private set
            {
                _model.LevelInt = value;
                RaisePropertyChanged();
            }
        }
    }
}