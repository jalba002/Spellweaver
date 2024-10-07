using Microsoft.Win32;
using Spellweaver.Data;
using Spellweaver.Services;

namespace Spellweaver.Managers
{
    public static class SpellExportWindowManager
    {
        private static string lastDirectory = Environment.CurrentDirectory;
        private static string extensionFilter = "Text files (*.txt)|*.txt|Json Files (*.json)|*.json";

        static SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = extensionFilter,
            InitialDirectory = lastDirectory,
            Title = "Export Spells"
        };

        static OpenFileDialog importFileDialog = new OpenFileDialog()
        {
            Filter = extensionFilter,
            InitialDirectory = lastDirectory,
            Title = "Import Spells"
        };

        public static void ExportSpells(List<Spell> spellsToExport)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                // Get the format somehow
                // Default for now is the Spellweaver format
                SpellIOService.ExportSpells(saveFileDialog.FileName, spellsToExport);
            }
        }

        public static IEnumerable<Spell> ImportSpells<T>() where T : BaseSpellModel
        {
            if (importFileDialog.ShowDialog() == true)
            {
                return SpellIOService.ImportSpells(saveFileDialog.FileName);
            }
            return new List<Spell>();
        }
    }
}
