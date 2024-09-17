using Spellweaver.Commands;

namespace Spellweaver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        private readonly SpellListViewModel? _listViewModel;
        private readonly SpellEditorViewModel? _editorViewModel;
        private readonly TitleViewModel? _titleViewModel;

        public MainViewModel(TitleViewModel defaultView, SpellListViewModel spellList, SpellEditorViewModel spellEditor)
        {
            _titleViewModel = defaultView;
            _listViewModel = spellList;
            _editorViewModel = spellEditor;

            SelectedViewModel = defaultView;

            LoadSpellEditorCommand = new DelegateCommand(LoadSpellEditor);
            LoadSpellListCommand = new DelegateCommand(LoadSpellList);
            LoadMainMenuCommand = new DelegateCommand(LoadMainMenu);
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
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        #region Commands

        public DelegateCommand LoadSpellEditorCommand { get; }
        public DelegateCommand LoadSpellListCommand { get; }
        public DelegateCommand LoadMainMenuCommand { get; }

        private void LoadSpellEditor(object? parameter)
        {
            SelectedViewModel = _editorViewModel;
        }

        private void LoadSpellList(object? parameter)
        {
            SelectedViewModel = _listViewModel;
        }

        private void LoadMainMenu(object? parameter)
        {
            SelectedViewModel = _titleViewModel;
        }

        #endregion
    }
}
