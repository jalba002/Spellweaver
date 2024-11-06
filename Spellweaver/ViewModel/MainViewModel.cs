using Microsoft.AspNetCore.Components;
using Spellweaver.Commands;
using Spellweaver.Managers;

namespace Spellweaver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SpellListViewModel ListViewModel;
        private readonly SpellEditorViewModel EditorViewModel;
        private readonly DownloaderViewModel DownloaderViewModel;

        private Lazy<TitleViewModel> TitleViewModel = new Lazy<TitleViewModel>();
        private Lazy<ConfigViewModel> ConfigViewModel = new Lazy<ConfigViewModel>();
        private Lazy<SpellCardViewModel> SpellCardViewModel = new Lazy<SpellCardViewModel>();
        private Lazy<NullSpellViewModel> NullSpellViewModel = new Lazy<NullSpellViewModel>();

        private ViewModelBase _selectedViewModel;

        private bool IsBusy { get; set; } = false;

        public MainViewModel(
            SpellListViewModel spellListVM,
            SpellEditorViewModel spellEditorVM,
            DownloaderViewModel downloaderVM)
        {
            this.ListViewModel = spellListVM;
            this.EditorViewModel = spellEditorVM;
            this.DownloaderViewModel = downloaderVM;

            LoadSpellEditorCommand = new AsyncCommand(LoadSpellEditor, CanExecute);
            LoadSpellListCommand = new DelegateCommand(LoadSpellList);
            LoadMainMenuCommand = new DelegateCommand(LoadMainMenu);
            LoadConfigCommand = new DelegateCommand(LoadConfig);
            LoadDownloaderControlCommand = new DelegateCommand(LoadDownloader);
            LoadSpellCardViewCommand = new AsyncCommand(LoadSpellCard, CanExecute);
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

        #region Commands

        public IAsyncCommand LoadSpellEditorCommand { get; }
        public DelegateCommand LoadSpellListCommand { get; }
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

        private void LoadSpellList(object? parameter)
        {
            SelectedViewModel = ListViewModel;
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
    }
}
