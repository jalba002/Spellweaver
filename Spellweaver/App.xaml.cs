﻿using Microsoft.Extensions.DependencyInjection;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Providers;
using Spellweaver.ViewModel;
using System.Windows;
using Serilog;
using Spellweaver.Interfaces;

namespace Spellweaver
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
             .WriteTo.File($"Logs/log.txt")
             .CreateLogger();

            ServiceCollection service = new ServiceCollection();
            ConfigureService(service);
            _serviceProvider = service.BuildServiceProvider();
            //Log.Information($"Starting Log for Version {Spellweaver.MainWindow.SpellweaverVersion}");
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddSerilog();
            
            service.AddTransient<MainWindow>();

            service.AddSingleton<MainViewModel>();

            service.AddSingleton<TitleViewModel>();

            service.AddSingleton<SpellEditorViewModel>();

            service.AddSingleton<SpellListViewModel>();

            service.AddSingleton<DownloaderViewModel>();

            service.AddSingleton<ConfigViewModel>();

            service.AddSingleton<SpellCardViewModel>();

            service.AddSingleton<DNDDatabase, OnlineDatabaseProvider>();

            service.AddSingleton<ErrorHandler>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}