using System.Text.Json.Serialization;

namespace Spellweaver.Data
{
    public class MobileAppSpellbook
    {
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("db")]
        public long? Db { get; set; }

        [JsonPropertyName("data")]
        public List<MobileAppSpellModel> Data { get; set; }

        public MobileAppSpellbook(List<Spell> spells)
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