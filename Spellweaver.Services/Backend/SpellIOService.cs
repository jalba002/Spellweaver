using Serilog;
using Spellweaver.Backend;
using Spellweaver.Data;

namespace Spellweaver.Services
{
    public static class SpellIOService
    {
        public static IEnumerable<Spell> ImportSpells(string filePath)
        {
            var contents = FileHandler.ReadFile(filePath);
            if (contents == null) return Enumerable.Empty<Spell>();
            Log.Information($"Importing from {filePath}\n{contents}");
            var spellResult = Serializer.Deserialize<IEnumerable<Spell>>(contents);
            if (spellResult == null) return Enumerable.Empty<Spell>();
            Log.Information($"Result from parsing is {spellResult}");
            return spellResult;
        }

        public static void ExportSpells(string filePath, List<Spell> spellsToExport)
        {
            Log.Debug($"There are {spellsToExport.Count}");
            var fullData = Serializer.Serialize(spellsToExport);
            Log.Information($"Data for {filePath} is:\n{fullData}");
            FileHandler.WriteFile(filePath, fullData);
        }
    }
}
