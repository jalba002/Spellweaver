using Spellweaver.Model;

namespace Spellweaver.ViewModel
{
    public class CastingTimeItemViewModel : ViewModelBase<CastingTime>
    {
        public CastingTimeItemViewModel(CastingTime model) : base(model)
        {

        }

        public string? Name
        {
            get => _model.Name;

            private set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }
    }
}