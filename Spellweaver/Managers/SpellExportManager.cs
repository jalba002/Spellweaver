using Microsoft.Win32;
using Spellweaver.Backend;
using Spellweaver.Data;
using System.IO;
using System.Text.Json;

namespace Spellweaver.Managers
{
    public static class SpellExportManager
    {
        private static string lastDirectory = Environment.CurrentDirectory;
        private static string extensionFilter = "Text files (*.txt)|*.txt|Json Files (*.json)|*.json";

        static SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = extensionFilter,
            InitialDirectory = lastDirectory,
        };

        static OpenFileDialog importFileDialog = new OpenFileDialog()
        {
            Filter = extensionFilter,
            InitialDirectory = lastDirectory,
        };

        private static JsonSerializerOptions defaultOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        public static void ExportSpells(IList<ExportableModel> spellsToExport)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                var fullData = Serializer.Serialize(spellsToExport, defaultOptions);
                StreamWriter stream = new StreamWriter(saveFileDialog.FileName);
                stream.Write(fullData);
                stream.Close();
            }
        }

        public static T? ImportSpell<T>() where T : ExportableModel
        {
            string? itemsString = GetFileText();
            if (itemsString == null) return null;
            // Maybe we can multiparse and set the way to import if we analyze the format first.
            // Read the first line, if it looks like something weird make sure we know what the fuck is it.
            //dynamic list = JsonSerializer.DeserializeObject<dynamic>(itemsString);
            //var spell = list[2].data;
            T? newLoadedSpell = JsonSerializer.Deserialize<T>(itemsString);
            return newLoadedSpell;
        }
        public static List<T>? ImportSpells<T>() where T : ExportableModel
        {
            string? itemsString = GetFileText();
            if (itemsString == null) return null;
            List<T>? newLoadedSpell = Serializer.Deserialize<List<T>>(itemsString, defaultOptions);
            return newLoadedSpell;
        }
        private static string? GetFileText()
        {
            if (importFileDialog.ShowDialog() == true)
            {
                var fileStream = importFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }
    }
}
