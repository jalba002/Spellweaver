using Spellweaver.Commands;
using Spellweaver.Managers;
using Spellweaver.Services.Backend;

namespace Spellweaver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get => instance;
            set
            {
                if(instance == null)
                    instance = value;
            }
        }

        private readonly SpellListViewModel ListViewModel;
        private readonly SpellEditorViewModel EditorViewModel;
        private readonly DownloaderViewModel DownloaderViewModel;

        private Lazy<TitleViewModel> TitleViewModel = new Lazy<TitleViewModel>();
        private Lazy<ConfigViewModel> ConfigViewModel = new Lazy<ConfigViewModel>();
        private Lazy<SpellCardViewModel> SpellCardViewModel = new Lazy<SpellCardViewModel>();
        private Lazy<NullSpellViewModel> NullSpellViewModel = new Lazy<NullSpellViewModel>();

        private UserConfigManager _userConfigManager;

        private StatusBarManager _statusBarManager;

        private bool IsBusy { get; set; } = false;

        public MainViewModel(
            SpellListViewModel spellListVM,
            SpellEditorViewModel spellEditorVM,
            DownloaderViewModel downloaderVM,
            ExporterFactory exporterFactory)
        {
            Instance = this;

            _userConfigManager = new UserConfigManager(exporterFactory);

            _statusBarManager = new StatusBarManager();

            this.ListViewModel = spellListVM;
            this.EditorViewModel = spellEditorVM;
            this.DownloaderViewModel = downloaderVM;

            LoadSpellEditorCommand = new AsyncCommand(LoadSpellEditor, CanExecute);
            LoadSpellListCommand = new AsyncCommand(LoadSpellList, CanExecute);
            LoadMainMenuCommand = new DelegateCommand(LoadMainMenu);
            LoadConfigCommand = new DelegateCommand(LoadConfig);
            LoadDownloaderControlCommand = new DelegateCommand(LoadDownloader);
            LoadSpellCardViewCommand = new AsyncCommand(LoadSpellCard, CanExecute);
        }

        private ViewModelBase _selectedViewModel;
        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public string StatusBarMessage
        {
            get => _statusBarManager.GetInfo();
            set
            {
                _statusBarManager.SetInfo(value);
                RaisePropertyChanged();
            }
        }

        #region Commands

        public IAsyncCommand LoadSpellEditorCommand { get; }
        public IAsyncCommand LoadSpellListCommand { get; }
        public DelegateCommand LoadMainMenuCommand { get; }
        public DelegateCommand LoadConfigCommand { get; }
        public DelegateCommand LoadDownloaderControlCommand { get; }
        public IAsyncCommand LoadSpellCardViewCommand { get; }

        private bool CanExecute() => !IsBusy;

        private async Task LoadSpellEditor()
        {
            if (IsBusy) return;
            await LoadViewModelIfSpellSelected(EditorViewModel);
        }

        private void LoadMainMenu(object? parameter)
        {
            SelectedViewModel = TitleViewModel.Value;
        }

        public void LoadConfig(object? parameter)
        {
            SelectedViewModel = ConfigViewModel.Value;
        }
        public void LoadDownloader(object? parameter)
        {
            SelectedViewModel = DownloaderViewModel;
        }

        private async Task LoadSpellList()
        {
            if (IsBusy) return;
            await ListViewModel.LoadAsync();
            SelectedViewModel = ListViewModel;
        }

        public async Task LoadSpellCard()
        {
            if (IsBusy) return;
            await LoadViewModelIfSpellSelected(SpellCardViewModel.Value);
        }

        private async Task LoadViewModelIfSpellSelected(ViewModelBase newViewModel)
        {
            await newViewModel.LoadAsync();
            if (!SpellManager.IsAnySpellSelected)
            {
                SelectedViewModel = NullSpellViewModel.Value;
            }
            else
            {
                SelectedViewModel = newViewModel;
            }
        }

        #endregion

        #region Load
        public override async Task LoadAsync()
        {
            _userConfigManager.Initialize();
            StatusBarMessage = "Loaded Main Window";
        }
        #endregion
    }
}
