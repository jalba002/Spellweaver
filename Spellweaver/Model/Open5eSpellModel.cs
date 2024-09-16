using Newtonsoft.Json;
using Spellweaver.Model.Exportables;

namespace Spellweaver.Model.Api
{
    public partial class Open5eSpellModel
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("next")]
        public Uri Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public Open5eSpellExportable[] Results { get; set; }
    }
}