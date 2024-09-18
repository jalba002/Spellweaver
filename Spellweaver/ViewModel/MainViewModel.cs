using Spellweaver.Commands;

namespace Spellweaver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        private readonly TitleViewModel? _titleViewModel;
        private readonly SpellListViewModel? _listViewModel;
        private readonly SpellEditorViewModel? _editorViewModel;
        private readonly ConfigViewModel? _configViewModel;

        public MainViewModel(TitleViewModel defaultView, SpellListViewModel spellList, SpellEditorViewModel spellEditor, ConfigViewModel configView)
        {
            _titleViewModel = defaultView;
            _listViewModel = spellList;
            _editorViewModel = spellEditor;
            _configViewModel = configView;

            LoadSpellEditorCommand = new DelegateCommand(LoadSpellEditor);
            LoadSpellListCommand = new DelegateCommand(LoadSpellList);
            LoadMainMenuCommand = new DelegateCommand(LoadMainMenu);
            LoadConfigCommand = new DelegateCommand(LoadConfig);
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
            await _titleViewModel.LoadAsync();
            await _listViewModel.LoadAsync();
            await _editorViewModel.LoadAsync();
            await _configViewModel.LoadAsync();
        }

        #region Commands

        public DelegateCommand LoadSpellEditorCommand { get; }
        public DelegateCommand LoadSpellListCommand { get; }
        public DelegateCommand LoadMainMenuCommand { get; }
        public DelegateCommand LoadConfigCommand { get; }

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

        public void LoadConfig(object? parameter)
        {
            SelectedViewModel = _configViewModel;
        }

        #endregion
    }
}
