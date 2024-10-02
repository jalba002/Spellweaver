//using Spellweaver.Backend;
//using Spellweaver.Data;

//namespace Spellweaver.Services
//{
//    public class SpellFrontToBackendWrapper : IInputOutputHandler<Spell>
//    {
//        public SpellFrontToBackendWrapper()
//        {

//        }

//        #region Commands and Functions

//        public Spell? ImportSingle()
//        {
//            var importedSpell = Spell.ImportSpell();
//            if (importedSpell == null) return null;
//            return new Spell(importedSpell);
//        }

//        public List<Spell>? ImportMultiple()
//        {
//            var importedSpells = SpellExporter.ImportSpells();
//            if (importedSpells == null) return null;

//            var returnval = new List<Spell>();
//            foreach (Spell spell in importedSpells)
//            {
//                returnval.Add(new Spell(spell));
//            }

//            if (returnval.Count <= 0) return null;
//            return returnval;
//        }

//        public void ExportSingle(Spell content)
//        {
//            GenericExportSpell(content);
//        }

//        public void ExportMultiple(List<Spell> content)
//        {
//            GenericExportSpell(content);
//        }

//        private void GenericExportSpell(Spell outSpell)
//        {
//            GenericExportSpell(new List<Spell>() { outSpell });
//        }

//        private void GenericExportSpell(List<Spell> outSpells)
//        {
//            var spells = outSpells;
//            // TODO Make this generic so we can export spells in different formats.
//            throw new NotImplementedException();
//            //switch (exportSettings.ExportType)
//            //{
//            //    case ExportationType.Spellweaver:
//            //        List<Spell> exportables = new List<Spell>();
//            //        foreach (var item in spells)
//            //        {
//            //            exportables.Add(item.GetModel);
//            //        }
//            //        if (exportables is not null && exportables.Count > 0)
//            //        {
//            //            SpellExporter.ExportSpells(exportables);
//            //        }
//            //        break;
//            //    case ExportationType.Open5e:
//            //        List<Open5eSpellExportable> exportables2 = new List<Open5eSpellExportable>();
//            //        foreach (var item in spells)
//            //        {
//            //            exportables2.Add(new Open5eSpellExportable(item.GetModel));
//            //        }
//            //        if (exportables2 is not null && exportables2.Count > 0)
//            //        {
//            //            SpellExporter.ExportSpells(exportables2);
//            //        }
//            //        break;
//            //    case ExportationType.EditionSpellbook5th:
//            //        List<Spellbook5eExportable> exportables3 = new List<Spellbook5eExportable>();
//            //        foreach (var item in spells)
//            //        {
//            //            exportables3.Add(new Spellbook5eExportable(item.GetModel));
//            //        }
//            //        if (exportables3 is not null && exportables3.Count > 0)
//            //        {
//            //            SpellExporter.ExportSpells(exportables3, ExportationType.EditionSpellbook5th);
//            //        }
//            //        break;
//            //    default:
//            //        break;
//            //}
//        }

//        #endregion
//    }
//}
