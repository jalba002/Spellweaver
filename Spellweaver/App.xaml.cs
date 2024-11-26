using Serilog;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Providers;
using Spellweaver.Services.Backend;
using Spellweaver.ViewModel;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Spellweaver.Interfaces;

namespace Spellweaver
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            //Log.Logger = new LoggerConfiguration()
            // .WriteTo.File($"log_{Spellweaver.MainWindow.SpellweaverVersion}.txt")
            // .CreateLogger();

            ServiceCollection service = new ServiceCollection();
            service.AddLogging();
            ConfigureService(service);
            _serviceProvider = service.BuildServiceProvider();
            //Log.Information($"Starting Log for Version {Spellweaver.MainWindow.SpellweaverVersion}");
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddSerilog();

            service.AddTransient<MainWindow>();

            // Needed windows because it needs a Database Provider.
            service.AddTransient<DownloaderViewModel>();
            service.AddTransient<SpellEditorViewModel>();
            service.AddTransient<SpellListViewModel>();

            service.AddTransient<UserConfigManager>();

            service.AddTransient<MainViewModel>();

            service.AddSingleton<DNDDatabase, OnlineDatabaseProvider>();

            service.AddTransient<IErrorHandler, BoxErrorHandler>();

            service.AddSingleton<Serializer>();

            service.AddSingleton<ExporterFactory>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Spellweaver.Properties.Settings.Default.Save();
        }
    }
}