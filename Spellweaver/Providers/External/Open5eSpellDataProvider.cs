using Spellweaver.Data;
using Spellweaver.Providers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace Spellweaver.Backend
{
    public class Open5eSpellDataProvider : IDataProvider<Spell>
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
        public Open5eSpellDataProvider()
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
        private async Task<Open5eSpellModel?> GetData(string url)
        {
            try
            {
                using HttpResponseMessage response = await _client.GetAsync(url);
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Open5eSpellModel>(jsonString, jsonSerializationOptions);
                }
            }
            catch (Exception ex)
            {
                //myCustomLogger.LogException(ex);
                MessageBox.Show($"Fooking error mate.\n{ex.Message}", "Error when Downloading", MessageBoxButton.OK);
            }
            return null;
        }

        private async Task<Open5eSpellModel> GetData(int page = 1)
        {
            try
            {
                using HttpResponseMessage response = await _client.GetAsync($"/v1/spells/?format={format}&page={page}");
                if (response != null)
                {
                    return await response.Content.ReadFromJsonAsync<Open5eSpellModel>(jsonSerializationOptions);
                    //return JsonSerializer.DeserializeAsync<Open5eSpellModel>(response, jsonSerializationOptions);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fooking error mate.\n{ex.Message}", "Error when Downloading", MessageBoxButton.OK);
            }
            return new Open5eSpellModel() { Results = Array.Empty<Open5eSpellExportable>() };
        }
        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            var unfilteredData = await GetData();
            List<Spell> result = new();
            foreach (Open5eSpellExportable spell in unfilteredData.Results)
            {
                result.Add(spell.TransformToInternalModel());
            }
            return result;
        }
    }
}