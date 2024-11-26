﻿using Spellweaver.ViewModel;
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

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            // We do this to set the global DataContext
            _spellweaverVersion = GetVersion();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private static string GetVersion()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return $"{version}";
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}