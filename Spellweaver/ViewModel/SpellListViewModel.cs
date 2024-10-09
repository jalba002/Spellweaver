using Serilog;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Managers;
using Spellweaver.Providers;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

namespace Spellweaver.ViewModel
{
    public class SpellListViewModel : ViewModelBase
    {
        private DNDDatabase _dbProvider;
        public SpellListViewModel(DNDDatabase dB)
        {
            _dbProvider = dB;

            // We must register all commands here as they are readonly.
            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            ShowSpellDebugCommand = new DelegateCommand(ShowDebugSpell);
            // Download spells
            DownloadSpellsCommand = new DelegateCommand(DownloadSpells);
            DownloadAllSpellsCommand = new DelegateCommand(DownloadAllSpells);
            // Export Spells Commands
            ExportSpellCommand = new DelegateCommand(ExportSpell);
            ExportSpellsCommand = new DelegateCommand(ExportSpells);
            // Import Spells Commands
            ImportSpellsCommand = new DelegateCommand(ImportSpells);
        }

        #region Collections
        public ObservableCollection<SpellItemViewModel> Spells { get; } = new();
        #endregion

        public SpellItemViewModel? SelectedSpell
        {
            get => SpellManager.CurrentSpell;
            set
            {
                SpellManager.CurrentSpell = value;
                RaisePropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public async override Task LoadAsync()
        {
        }

        #region Commands
        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ShowSpellDebugCommand { get; }
        public DelegateCommand DownloadSpellsCommand { get; }
        public DelegateCommand DownloadAllSpellsCommand { get; }
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        //public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        private void Add(object? parameter)
        {
            var spell = new Spell { Name = "Default Spell", Level = "0", CastingTime = "1 action" };
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
        // This is binded to the command to open the window and then importSpells from a file.
        // It import any amount, as it is always exported as an array.
        private void ImportSpells(object? parameter)
        {
            var importResult = SpellExportWindowManager.ImportSpells<Spell>();
            Log.Information($"Adding {importResult.ToArray().Length} new spells from import mode");
            foreach (var item in importResult)
            {
                ImportSpell(item);
            }
        }

        private void ImportSpell(Spell? spell)
        {
            if (spell is not null)
            {
                //Spells.Add(new SpellItemViewModel(spell.TransformToInternalModel()));
                Spells.Add(new SpellItemViewModel(spell));
            }
        }

        private async void DownloadSpells(object? parameter)
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            var spellsResult = await _dbProvider.GetSpellsAsync();
            if (spellsResult.Count < 0)
            {
                MessageBox.Show("No spells received when importing");
                return;
            }
            UpdateSpellList(spellsResult);
        }

        private async void DownloadAllSpells(object? parameter)
        {
            // Here we call the BACKEND to gather some information for us.
            //var importedSpells = SpellOnlineImporter.GetAllAsync();
            var spellsResult = await ((OnlineDatabaseProvider)_dbProvider).GetALLSpellsAsync();
            if (spellsResult.Count < 0)
            {
                MessageBox.Show("No spells received when importing");
                return;
            }
            UpdateSpellList(spellsResult);
        }

        private void UpdateSpellList(List<Spell> spellList)
        {
            SelectedSpell = null;
            Spells.Clear();
            foreach (var externalSpell in spellList)
            {
                Spells.Add(new SpellItemViewModel(externalSpell));
            }
        }

        private void ExportSpell(object? parameter)
        {
            if (SelectedSpell == null) return;
            SpellExportWindowManager.ExportSpells(new List<Spell>() { SelectedSpell.GetModel.TransformToInternalModel() });
        }
        private void ExportSpells(object? parameter)
        {
            List<Spell> exportables = new List<Spell>();
            foreach (var item in Spells)
            {
                exportables.Add(item.GetModel.TransformToInternalModel());
            }
            SpellExportWindowManager.ExportSpells(exportables);
        }

        private bool CanRemove(object? parameter) => SelectedSpell is not null;
        private void ShowDebugSpell(object? parameter)
        {
            if (SelectedSpell is not null)
            {
                MessageBox.Show(JsonSerializer.Serialize(SelectedSpell.GetModel), "Debugging Spell");
            }
            else
            {
                MessageBox.Show("Select a spell first.", "SelectedSpell is null", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
    }
}