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

        public static void Export(string exportString)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                // Get the format somehow
                // Default for now is the Spellweaver format

                FileHandler.WriteFile(saveFileDialog.FileName, exportString);
            }
        }

        public static string? Import()
        {
            if (importFileDialog.ShowDialog() == true)
            {
                // Get the format somehow
                // Default for now is the Spellweaver format

                return FileHandler.ReadFile(saveFileDialog.FileName);
            }
            return null;
        }
    }
}
