using System.Text.Json.Serialization;

namespace Spellweaver.Data.Models.Structures
{
    public partial class OpenFifthEditionRequestResponse
    {
        [JsonPropertyName("count")]
        public long? Count { get; set; }

        [JsonPropertyName("next")]
        public Uri? Next { get; set; }

        [JsonPropertyName("previous")]
        public object? Previous { get; set; }

        [JsonPropertyName("results")]
        public OpenFifthEditionSpellModel[] Results { get; set; }
    }
}