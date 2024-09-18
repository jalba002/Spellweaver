using Microsoft.Extensions.DependencyInjection;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Model;
using Spellweaver.ViewModel;
using System.Windows;

namespace Spellweaver
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection service = new ServiceCollection();
            ConfigureService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddTransient<MainWindow>();

            service.AddSingleton<MainViewModel>();

            service.AddSingleton<TitleViewModel>();

            service.AddSingleton<SpellEditorViewModel>();

            service.AddSingleton<SpellListViewModel>();

            service.AddSingleton<SpellManager>();

            service.AddSingleton<ConfigViewModel>();

            service.AddTransient<IDBProvider<DNDDatabase>, DefaultDatabaseProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}