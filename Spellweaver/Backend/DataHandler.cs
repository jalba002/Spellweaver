using Spellweaver.Interfaces;
using Spellweaver.Data;
using Spellweaver.ViewModel;
using Spellweaver.ViewModel.Items;

namespace Spellweaver.Backend
{
    public struct ExportSettings
    {
        public ExportationType ExportType;
    }

    public class DataHandler : IInputOutputHandler<SpellItemViewModel>
    {
        public DataHandler()
        {

        }

        #region Commands and Functions

        public SpellItemViewModel? ImportSingle()
        {
            var importedSpell = SpellExporter.ImportSpell();
            if (importedSpell == null) return null;
            return new SpellItemViewModel(importedSpell);
        }

        public List<SpellItemViewModel>? ImportMultiple()
        {
            var importedSpells = SpellExporter.ImportSpells();
            if (importedSpells == null) return null;

            var returnval = new List<SpellItemViewModel>();
            foreach (Spell spell in importedSpells)
            {
                returnval.Add(new SpellItemViewModel(spell));
            }

            if (returnval.Count <= 0) return null;
            return returnval;
        }

        public void ExportSingle(SpellItemViewModel content, ExportSettings exportSettings)
        {
            GenericExportSpell(content, exportSettings);
        }

        public void ExportMultiple(List<SpellItemViewModel> content, ExportSettings exportSettings)
        {
            GenericExportSpell(content, exportSettings);
        }

        private void GenericExportSpell(SpellItemViewModel outSpell, ExportSettings exportSettings)
        {
            GenericExportSpell(new List<SpellItemViewModel>() { outSpell }, exportSettings);
        }

        private void GenericExportSpell(List<SpellItemViewModel> outSpells, ExportSettings exportSettings)
        {
            var spells = outSpells;
            switch (exportSettings.ExportType)
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
    }
}
