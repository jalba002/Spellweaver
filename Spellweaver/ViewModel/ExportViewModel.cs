using Spellweaver.Backend;
using Spellweaver.Commands;
using Spellweaver.Converter;
using Spellweaver.Data;
using Spellweaver.ViewModel.Items;
using System.Collections.ObjectModel;

namespace Spellweaver.ViewModel
{
    public class ExportViewModel : ViewModelBase
    {
        #region Exportable Settings
        private ExportableTypeItemViewModel? _selectedExportationType;

        public ExportableTypeItemViewModel? SelectedExportType
        {
            get => _selectedExportationType;
            set
            {
                _selectedExportationType = value;
                RaisePropertyChanged();
            }
        }
        // Here we have the types of exportation modes. Not yet we export.
        public ObservableCollection<ExportableTypeItemViewModel> ExportTypes { get; } = new();
        #endregion

        // a reference to the big one.

        public ExportViewModel()
        {
            AddExportTypes();
        }

        #region Commands and Functions
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        

        #endregion

        private void AddExportTypes()
        {
            ExportTypes.Add(new ExportableTypeItemViewModel()
            {
                ExportationType = ExportationType.Spellweaver
            });
            ExportTypes.Add(new ExportableTypeItemViewModel()
            {
                ExportationType = ExportationType.EditionSpellbook5th
            });
            ExportTypes.Add(new ExportableTypeItemViewModel()
            {
                ExportationType = ExportationType.Open5e
            });
        }
    }
}
