using Serilog;
using Spellweaver.Data;
using Spellweaver.Data.Models.Structures;
using System.Diagnostics;
using System.Text.Json;

namespace Spellweaver.Services
{
    public class O5ESpellDataProvider : IDataProvider<Spell>
    {
        protected HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.open5e.com")
        };

        protected const string format = "json";

        protected readonly JsonSerializerOptions jsonSerializationOptions = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
        };

        public async Task<IEnumerable<Spell>> GetAllDatabase()
        {
            string url = $"v1/spells/?format={format}";
            bool IsValid = true;
            List<Spell> result = new List<Spell>();
            do
            {
                var httpResult = await GetData(url);
                if (httpResult == null)
                {
                    // WE might want to log that.
                    Log.Error($"Request from website {url} was null.");
                    break;
                }
                foreach (var unparsedSpell in httpResult.Results)
                {
                    result.Add(unparsedSpell.TransformToInternalModel());
                }
                IsValid = httpResult.Next != null;
                if (IsValid)
                    url = httpResult.Next.ToString();
            }
            while (IsValid);
            return result;
        }

        private async Task<O5ERequestResponse?> GetData(string url)
        {
            using HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<O5ERequestResponse>(jsonString, jsonSerializationOptions);
            }
            return new O5ERequestResponse() { Results = Array.Empty<O5ESpellModel>(), Next = null };
        }

        private async Task<O5ERequestResponse?> GetDataFromPage(int page = 1)
        {
            string newUrl = $"v1/spells/?format={format}&page={page}";
            return await GetData(newUrl);
        }

        public async Task<O5ERequestResponse?> GetSpellMatch(string match)
        {
            match = match.Replace("\n", "%20");
            string newUrl = $"v1/spells/?dnd_class={match}&limit=5";
            return await GetData(newUrl);
        }

        public async Task<O5ERequestResponse?> GetSpellWithMultipleParameters(Dictionary<string, string> keyValueParameters)
        {
            string newUrl = "v1/spells/?";
            List<string> parameterList = new List<string>();
            foreach (var entry in keyValueParameters)
            {
                var valueEntry = entry.Value.Replace("\n", "%20");
                var keyEntry = entry.Key;
                parameterList.Add($"{keyEntry}={valueEntry}");
            }
            newUrl += string.Join("&", parameterList);
            Trace.WriteLine(newUrl);
            return await GetData(newUrl);
        }

        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            var unfilteredData = await GetDataFromPage();
            List<Spell> result = new();
            foreach (O5ESpellModel spell in unfilteredData.Results)
            {
                result.Add(spell.TransformToInternalModel());
            }
            return result;
        }
    }
}