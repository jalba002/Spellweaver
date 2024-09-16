namespace Spellweaver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SpellsViewModel _spellsViewModel;
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(SpellsViewModel spellsViewModel)
        {
            _spellsViewModel = spellsViewModel;
            SelectedViewModel = _spellsViewModel;
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public async override Task LoadAsync()
        {
            if(SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
    }
}
