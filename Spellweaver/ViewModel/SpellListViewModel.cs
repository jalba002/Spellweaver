﻿using Newtonsoft.Json;
using Spellweaver.Backend;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Interfaces;
using Spellweaver.Managers;
using Spellweaver.Model;
using Spellweaver.ViewModel.Items;
using System.Collections.ObjectModel;
using System.Windows;

namespace Spellweaver.ViewModel
{
    public class SpellListViewModel : ViewModelBase
    {
        private SpellManager _spellManager;
        private IInputOutputHandler<SpellItemViewModel> _handler;
        private IDBProvider<DNDDatabase> _dbProvider;
        public SpellListViewModel(SpellManager spellManager, DataHandler dataHandler, IDBProvider<DNDDatabase> dB)
        {
            _spellManager = spellManager;
            _handler = dataHandler;
            _dbProvider = dB;

            // We must register all commands here as they are readonly.
            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            ShowSpellDebugCommand = new DelegateCommand(ShowDebugSpell);
            // Download spells
            DownloadSpellsCommand = new DelegateCommand(DownloadSpells);
            // Export Spells Commands
            ExportSpellCommand = new DelegateCommand(ExportSpell);
            ExportSpellsCommand = new DelegateCommand(ExportSpells);
            // Import Spells Commands
            ImportSpellCommand = new DelegateCommand(ImportSpell);
            ImportSpellsCommand = new DelegateCommand(ImportSpells);
        }

        #region Collections
        public ObservableCollection<SpellItemViewModel> Spells { get; } = new();
        #endregion
        public string SpellCount => $"X/{Spells.Count}";

        public SpellItemViewModel? SelectedSpell
        {
            get => _spellManager.CurrentSpell;
            set
            {
                _spellManager.CurrentSpell = value;
                RaisePropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public async override Task LoadAsync()
        {
            _dbProvider.OnDatabaseLoaded += (DNDDatabase db) =>
            {
                foreach (Spell spell in db.Spells)
                {
                    Spells.Add(new SpellItemViewModel(spell));
                }
            };
        }

        #region Commands
        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ShowSpellDebugCommand { get; }
        public DelegateCommand DownloadSpellsCommand { get; }
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        private void Add(object? parameter)
        {
            var spell = new Spell { Name = "Default Spell", Level = "Cantrip", CastingTime = "1 action" };
            var viewModel = new SpellItemViewModel(spell);
            Spells.Add(viewModel);
            SelectedSpell = viewModel;
        }
        private void Remove(object? parameter)
        {
            if (SelectedSpell is not null)
            {
                MessageBoxResult m = MessageBox.Show("Are you sure you want to remove " + SelectedSpell.Name + " from the list?", "Removing Spell", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    Spells.Remove(SelectedSpell);
                    SelectedSpell = null;
                }
            }
        }
        private void ImportSpell(object? parameter)
        {
            //ImportSpell();
        }
        private void ImportSpells(object? parameter)
        {
            // ??
        }

        public void ImportSpell(Spell? spell)
        {
            if (spell is not null)
            {
                //Spells.Add(new SpellItemViewModel(spell.TransformToInternalModel()));
                Spells.Add(new SpellItemViewModel(spell));
            }
        }
        public void ImportSpells(List<Spell>? spells)
        {
            // Show messagebox asking if it wants to replace current Database or Merge.
            if (spells is null) return;

            var messageBox = MessageBox.Show("Do you want to replace the current spell list (No)\nor merge the new list into the current list (Yes).", "Importing Database", MessageBoxButton.YesNoCancel);
            if (messageBox == MessageBoxResult.No)
            {
                // Delete the current spells
                Spells.Clear();
            }
            else if (messageBox == MessageBoxResult.Cancel)
            {
                // Canceled completely. Stays as is.
                return;
            }

            foreach (var spell in spells)
            {
                if (spell is not null)
                {
                    Spells.Add(new SpellItemViewModel(spell));
                }
            }
        }
        private async void DownloadSpells(object? parameter)
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            var spellsResult = _dbProvider.GetInstance.Spells;
            if (spellsResult is not null)
            {
                if (spellsResult.Count < 0)
                {
                    MessageBox.Show("No spells received when importing");
                    return;
                }
                SelectedSpell = null;
                Spells.Clear();
                foreach (var externalSpell in spellsResult)
                {
                    Spells.Add(new SpellItemViewModel(externalSpell));
                }
            }
        }
        private void ExportSpell(object? parameter)
        {
            //SpellItemViewModel? selectedSpell = _mainViewModel.SelectedSpell;
            //List<SpellItemViewModel> list = new List<SpellItemViewModel>();
            //if (selectedSpell is not null)
            //{
            //    // Call backend for a spell exportation!
            //    list.Add(selectedSpell);
            //    GenericExportSpell(list);
            //}
        }
        private void ExportSpells(object? parameter)
        {
            _handler.ExportMultiple(Spells.ToList(), new ExportSettings() { ExportType = ExportationType.Spellweaver});
        }

        private bool CanRemove(object? parameter) => SelectedSpell is not null;
        private void ShowDebugSpell(object? parameter)
        {
            if (SelectedSpell is not null)
            {
                MessageBox.Show(JsonConvert.SerializeObject(SelectedSpell.GetModel, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented), "Debugging Spell");
            }
            else
            {
                MessageBox.Show("Select a spell first.", "SelectedSpell is null", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
    }
}