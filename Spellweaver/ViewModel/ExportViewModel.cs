using Spellweaver.Backend;
using Spellweaver.Commands;
using Spellweaver.Converter;
using Spellweaver.Model;
using Spellweaver.Model.Exportables;
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
        private readonly SpellEditorViewModel _mainViewModel;

        public ExportViewModel(SpellEditorViewModel mainViewModel)
        {
            AddExportTypes();

            _mainViewModel = mainViewModel;

            ExportSpellCommand = new DelegateCommand(ExportSpell);
            ExportSpellsCommand = new DelegateCommand(ExportSpells);
            // Import?
            ImportSpellCommand = new DelegateCommand(ImportSpell);
            ImportSpellsCommand = new DelegateCommand(ImportSpells);
        }

        #region Commands and Functions
        public DelegateCommand ExportSpellCommand { get; }
        public DelegateCommand ExportSpellsCommand { get; }
        public DelegateCommand ImportSpellCommand { get; }
        public DelegateCommand ImportSpellsCommand { get; }

        private void ImportSpell(object? parameter)
        {
            Spell? spell = SpellExporter.ImportSpell<Spell>();
            // Tell the mainapp to import spell

        }
        private void ImportSpells(object? parameter)
        {
            // Show messagebox asking if it wants to replace current Database or Merge.
            List<Spell>? spells = SpellExporter.ImportSpells<Spell>() as List<Spell>;
            //_mainViewModel.ImportSpells(spells);
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
            //GenericExportSpell(_mainViewModel.Spells.ToList());
        }
        private void GenericExportSpell(List<SpellItemViewModel> outSpells)
        {
            if (SelectedExportType is null) { return; }
            var spells = outSpells;
            switch (SelectedExportType.ExportationType)
            {
                case ExportationType.Spellweaver:
                    List<Spell> exportables = new List<Spell>();
                    foreach (var item in spells)
                    {
                        exportables.Add(item.GetModel);
                    }
                    if (exportables is not null && exportables.Count > 0)
                    {
                        SpellExporter.ExportSpells(exportables);
                    }
                    break;
                case ExportationType.Open5e:
                    List<Open5eSpellExportable> exportables2 = new List<Open5eSpellExportable>();
                    foreach (var item in spells)
                    {
                        exportables2.Add(new Open5eSpellExportable(item.GetModel));
                    }
                    if (exportables2 is not null && exportables2.Count > 0)
                    {
                        SpellExporter.ExportSpells(exportables2);
                    }
                    break;
                case ExportationType.EditionSpellbook5th:
                    List<Spellbook5eExportable> exportables3 = new List<Spellbook5eExportable>();
                    foreach (var item in spells)
                    {
                        exportables3.Add(new Spellbook5eExportable(item.GetModel));
                    }
                    if (exportables3 is not null && exportables3.Count > 0)
                    {
                        SpellExporter.ExportSpells(exportables3, ExportationType.EditionSpellbook5th);
                    }
                    break;
                default:
                    break;
            }
        }

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
