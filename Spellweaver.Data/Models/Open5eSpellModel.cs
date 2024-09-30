using System.Text.Json.Serialization;

namespace Spellweaver.Data
{
    public partial class Open5eSpellModel
    {
        [JsonPropertyName("count")]
        public long? Count { get; set; }

        [JsonPropertyName("next")]
        public Uri? Next { get; set; }

        [JsonPropertyName("previous")]
        public object? Previous { get; set; }

        [JsonPropertyName("results")]
        public Open5eSpellExportable[] Results { get; set; }
    }
}