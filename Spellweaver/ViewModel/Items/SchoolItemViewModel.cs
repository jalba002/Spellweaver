using Spellweaver.Model;

namespace Spellweaver.ViewModel
{
    public class SchoolItemViewModel : ViewModelBase
    {
        private readonly School _model;
        public SchoolItemViewModel(School model)
        {
            _model = model;
        }
        public School GetModel => _model;
        public string? Name
        {
            get => _model.Name;

            private set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }
        public string? Description
        {
            get => _model.Description;

            private set
            {
                _model.Description = value;
                RaisePropertyChanged();
            }
        }
        public string? ColorCode
        {
            get => _model.ColorCode;

            private set
            {
                _model.ColorCode = value;
                RaisePropertyChanged();
            }
        }
    }
}
