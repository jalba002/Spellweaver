using Spellweaver.Data;
using Spellweaver.Data.Models.Structures;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spellweaver.Services
{
    public class OpenFifthEditionSpellDataProvider : IDataProvider<Spell>
    {
        private HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.open5e.com")
        };

        private readonly JsonSerializerOptions jsonSerializationOptions = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            //UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
        };

        private const string format = "json";
        public OpenFifthEditionSpellDataProvider()
        {

        }

        public async Task<IEnumerable<Spell>> GetAllDatabase()
        {
            string url = $"/v1/spells/?format={format}";
            bool IsValid = true;
            List<Spell> result = new List<Spell>();
            do
            {
                var httpResult = await GetData(url);
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

        // Non-generic stuff, but for now it will work. This can be the "ONLINE" One, which pulls from a set website
        // With a set structure.
        private async Task<FifthEditionSpellModel?> GetData(string url)
        {
            using HttpResponseMessage response = await _client.GetAsync(url);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<FifthEditionSpellModel>(jsonString, jsonSerializationOptions);
            }
            return null;
        }

        private async Task<OpenFifthEditionRequestResponse?> GetData(int page = 1)
        {
            using HttpResponseMessage response = await _client.GetAsync($"/v1/spells/?format={format}&page={page}");
            if (response != null)
            {
                return await response.Content.ReadFromJsonAsync<OpenFifthEditionRequestResponse>(jsonSerializationOptions);
                //return JsonSerializer.DeserializeAsync<Open5eSpellModel>(response, jsonSerializationOptions);
            }
            return new OpenFifthEditionRequestResponse() { Results = Array.Empty<FifthEditionSpellModel>() };
        }
        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            var unfilteredData = await GetData();
            List<Spell> result = new();
            foreach (OpenFifthEditionRequestResponse spell in unfilteredData.Results)
            {
                result.Add(spell.TransformToInternalModel());
            }
            return result;
        }
    }
}