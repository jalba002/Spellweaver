using Newtonsoft.Json;
using Spellweaver.Backend;
using Spellweaver.Commands;
using Spellweaver.Data;
using Spellweaver.Model;
using Spellweaver.Model.Api;
using Spellweaver.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Spellweaver.ViewModel
{
    public class SpellListViewModel : ViewModelBase
    {
        private readonly IDBProvider<DNDDatabase> _databaseProvider;
        private SpellItemViewModel? _selectedSpell;
        public SpellListViewModel(IDBProvider<DNDDatabase> databaseProvider)
        {
            _databaseProvider = databaseProvider;

            // We must register all commands here as they are readonly.
            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            ShowSpellDebugCommand = new DelegateCommand(ShowDebugSpell);
            // Download spells
            DownloadSpellsCommand = new DelegateCommand(DownloadSpells);
        }


        #region Collections
        public ObservableCollection<SpellItemViewModel> Spells { get; } = new();
        #endregion

        public SpellItemViewModel? SelectedSpell
        {
            get => _selectedSpell;
            set
            {
                _selectedSpell = value;
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
        public DelegateCommand OpenExportCommand { get; }
        public DelegateCommand DownloadSpellsCommand { get; }

        private void Add(object? parameter)
        {
            var spell = new Spell { Name = "New Spell" };
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
            var result = await SpellOnlineImporter.GetAllAsync();
            var formatted = JsonConvert.DeserializeObject<Open5eSpellModel>(result);
            if (formatted is not null)
            {
                if (formatted.Results is null || formatted.Results.Length < 0)
                {
                    MessageBox.Show("Error when importing externals! :(");
                    return;
                }
                SelectedSpell = null;
                Spells.Clear();
                foreach (var externalSpell in formatted.Results)
                {
                    Spells.Add(new SpellItemViewModel(externalSpell.TransformToInternalModel()));
                }
            }
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