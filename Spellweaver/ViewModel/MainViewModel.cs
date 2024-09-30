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
        private readonly DownloaderViewModel? _downloaderViewModel;

        public MainViewModel(
            TitleViewModel defaultView, 
            SpellListViewModel spellList, 
            SpellEditorViewModel spellEditor, 
            ConfigViewModel configView,
            DownloaderViewModel downloaderViewModel)
        {
            _titleViewModel = defaultView;
            _listViewModel = spellList;
            _editorViewModel = spellEditor;
            _configViewModel = configView;
            _downloaderViewModel = downloaderViewModel;

            SelectedViewModel = _titleViewModel;

            LoadSpellEditorCommand = new DelegateCommand(LoadSpellEditor);
            LoadSpellListCommand = new DelegateCommand(LoadSpellList);
            LoadMainMenuCommand = new DelegateCommand(LoadMainMenu);
            LoadConfigCommand = new DelegateCommand(LoadConfig);
            LoadDownloaderControlCommand = new DelegateCommand(LoadDownloader);
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
            await _downloaderViewModel.LoadAsync();
        }

        #region Commands

        public DelegateCommand LoadSpellEditorCommand { get; }
        public DelegateCommand LoadSpellListCommand { get; }
        public DelegateCommand LoadMainMenuCommand { get; }
        public DelegateCommand LoadConfigCommand { get; }
        public DelegateCommand LoadDownloaderControlCommand { get; }

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
        public void LoadDownloader(object? parameter)
        {
            SelectedViewModel = _downloaderViewModel;
        }

        #endregion
    }
}
