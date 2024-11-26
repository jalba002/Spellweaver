using Spellweaver.Services;
using Spellweaver.Services.Backend;
using System.IO;
using System.Windows;

namespace Spellweaver.Managers
{
    public class UserConfigManager
    {
        private string AppFolder => Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "SpellWeaver");

        private string LastSpellListStored => Path.Combine(AppFolder, "lastSpellList.json");

        private ExporterFactory _iomanager;

        public UserConfigManager(ExporterFactory ioMan)
        {
            _iomanager = ioMan;
        }

        public void Initialize()
        {
            CreateDefaultFolder();
            App.Current.MainWindow.Closed += Save;
            Load();
        }

        private void CreateDefaultFolder()
        {
            if(!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }

        public void Load()
        {
            LoadLastSpellList();
        }

        private bool LoadLastSpellList()
        {
            if (!File.Exists(LastSpellListStored)) return false;

            var tempValue = _iomanager.Import(FileHandler.ReadFile(LastSpellListStored), ExporterFactory.ExportType.Spellweaver);

            if (tempValue == null) return false;

            SpellManager.SetSpellList(tempValue);
            return true;
        }

        public void Save(object? sender, EventArgs e)
        {
            // save the spell list
            var spellList = _iomanager.Export(SpellManager.GetSpellList().ToList(), ExporterFactory.ExportType.Spellweaver);
            FileHandler.WriteFile(LastSpellListStored, spellList);

            // save the window size?
        }
    }
}
