using Serilog;
using Spellweaver.ViewModel;
using System.Configuration;
using System.Windows;

namespace Spellweaver
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private static string _spellweaverVersion = GetVersion();
        public static string SpellweaverVersion
        {
            get => _spellweaverVersion;
        }

        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            // Get the Window Settings?
            LoadSettings();

            // We do this to set the global DataContext
            _spellweaverVersion = GetVersion();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
            Log.Information($"Started main window of Spellweaver");
        }

        private void LoadSettings()
        {

        }

        private static string GetVersion()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                                    .AddDays(version.Build).AddSeconds(version.Revision * 2);
            return $"{version}";
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}