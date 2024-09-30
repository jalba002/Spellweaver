using Microsoft.Win32;
using Spellweaver.Data;
using Spellweaver.ViewModel.Items;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Spellweaver.Backend
{
    public static class SpellExporter
    {
        public static SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            InitialDirectory = Environment.CurrentDirectory,
        };
        public static OpenFileDialog importFileDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.CurrentDirectory,
        };
        public static void ExportSpells(IList spellsToExport, ExportationType exportMode = ExportationType.Spellweaver)
        {
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|Json Files (*.json)|*.json";
            saveFileDialog.DefaultExt = ".txt";
            string fullData = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                switch (exportMode)
                {
                    case ExportationType.Spellweaver:
                        fullData = JsonSerializer.Serialize(spellsToExport);
                        break;
                    case ExportationType.Open5e:
                        fullData = JsonSerializer.Serialize(spellsToExport);
                        break;
                    case ExportationType.EditionSpellbook5th:
                        Spellbook5ThEdition exportModel = new Spellbook5ThEdition()
                        {
                            Data = (List<Spellbook5eExportable>)spellsToExport,
                            Version = "3.1.5",
                            Db = 13
                        };
                        fullData = JsonSerializer.Serialize(exportModel);
                        break;
                }

                StreamWriter stream = new StreamWriter(saveFileDialog.FileName);
                stream.Write(fullData);
                stream.Close();
            }
        }
        public static Spell? ImportSpell()
        {
            string? itemsString = GetFileText();
            if (itemsString == null) return null;
            try
            {
                // Maybe we can multiparse and set the way to import if we analyze the format first.
                // Read the first line, if it looks like something weird make sure we know what the fuck is it.
                //dynamic list = JsonSerializer.DeserializeObject<dynamic>(itemsString);
                //var spell = list[2].data;
                Spell? newLoadedSpell = JsonSerializer.Deserialize<Spell>(itemsString);
                return newLoadedSpell;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error when importing the spell data. It might have an incorrect format or the file was modified.", "Import Error", MessageBoxButton.OK, MessageBoxImage.Warning);
#if DEBUG
                Console.WriteLine("ERROR: " + e.Message);
#endif
            }
            return null;
        }
        public static List<Spell>? ImportSpells()
        {
            string? itemsString = GetFileText();
            if (itemsString == null) return null;
            try
            {
                //dynamic list = JsonSerializer.DeserializeObject<dynamic>(itemsString);
                //var spell = list[2].data;
                List<Spell>? newLoadedSpell = JsonSerializer.Deserialize(itemsString, typeof(List<Spell>)) as List<Spell>;
                return newLoadedSpell;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error when importing the spell data. It might have an incorrect format or the file was modified.", "Import Error", MessageBoxButton.OK, MessageBoxImage.Warning);
#if DEBUG
                Console.WriteLine("ERROR: " + e.Message);
#endif
            }
            return null;
        }
        private static string? GetFileText()
        {
            importFileDialog.Filter = "Text files (*.txt)|*.txt|Json Files (*.json)|*.json"; //|All files (*.*)|*.*";

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
