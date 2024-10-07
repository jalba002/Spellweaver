using Spellweaver.Data.Models.Exportables.Collections;
using System.Text.Json.Serialization;


namespace Spellweaver.Data
{
    public class MobileAppExportStructure : ExportableSpellbook
    {
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("db")]
        public long? Db { get; set; }

        [JsonPropertyName("data")]
        public List<MobileAppSpellModel> Data { get; set; }

        public MobileAppExportStructure(List<Spell> spells)
        {
            Db = 13;
            Version = "3.1.5";
            Data = new();
            foreach (Spell spell in spells)
            {
                Data.Add(new MobileAppSpellModel(spell));
            }
        }
    }
}