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
    public class SpellEditorViewModel : ViewModelBase
    {
        private SpellItemViewModel? _selectedSpell;
        public SpellEditorViewModel()
        {

        }


        #region Collections
        public ObservableCollection<SchoolItemViewModel> Schools { get; } = new();
        public ObservableCollection<CastingTimeItemViewModel> CastingTimes { get; } = new();
        public ObservableCollection<LevelItemViewModel> Levels { get; } = new();
        #endregion
        public SpellItemViewModel? SelectedSpell
        {
            get => _selectedSpell;
            set
            {
                _selectedSpell = value;
                RaisePropertyChanged();
            }
        }

        public async override Task LoadAsync()
        {
            //if (!Schools.Any())
            //{
            //    var schools = (await Application.Current.MainWindow._viewModel..GetAllAsync())?.Schools;
            //    if (schools is not null)
            //    {
            //        foreach (var school in schools)
            //        {
            //            Schools.Add(new SchoolItemViewModel(school));
            //        }
            //    }
            //}
            //if (!CastingTimes.Any())
            //{
            //    var castingTimes = (await _databaseProvider.GetAllAsync())?.CastingTimes;
            //    if (castingTimes is not null)
            //    {
            //        foreach (var castingTime in castingTimes)
            //        {
            //            CastingTimes.Add(new CastingTimeItemViewModel(castingTime));
            //        }
            //    }
            //}
            //if (!Levels.Any())
            //{
            //    var levels = (await _databaseProvider.GetAllAsync())?.Levels;
            //    if (levels is not null)
            //    {
            //        foreach (var level in levels)
            //        {
            //            Levels.Add(new LevelItemViewModel(level));
            //        }
            //    }
            //}
        }

        #region Commands

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