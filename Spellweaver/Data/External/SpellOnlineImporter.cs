using Newtonsoft.Json;
using Spellweaver.Data;
using Spellweaver.Model;
using Spellweaver.Model.Api;
using Spellweaver.Model.Exportables;
using System.Net.Http;
using System.Windows;

namespace Spellweaver.Backend
{
    public class Open5eSpellDataProvider : IDataProvider<Spell>
    {
        private HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.open5e.com")
        };

        private const string format = "json";
        public Open5eSpellDataProvider()
        {

        }

        public async Task<IEnumerable<Spell>> GetAllDatabase()
        {
            string url = $"/v1/spells/?format={format}";
            List<Spell> result = new List<Spell>();
            do
            {
                var httpResult = await GetData(url);
                foreach (var unparsedSpell in httpResult.Results)
                {
                    result.Add(unparsedSpell.TransformToInternalModel());
                }
                url = httpResult.Next.ToString();
            }
            while (url != null);
            return result;
        }

        private async Task<Open5eSpellModel?> GetData(string url)
        {
            try
            {
                using HttpResponseMessage response = await _client.GetAsync(url);
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Open5eSpellModel>(jsonString);
                }
            }
            catch (Exception ex)
            {
                //myCustomLogger.LogException(ex);
                MessageBox.Show($"Fooking error mate.\n{ex.Message}", "Error when Downloading", MessageBoxButton.OK);
            }
            return null;
        }

        private async Task<Open5eSpellModel?> GetData(int page = 1)
        {
            try
            {
                using HttpResponseMessage response = await _client.GetAsync($"/v1/spells/?format={format}&page={page}");
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Open5eSpellModel>(jsonString);
                }
            }
            catch (Exception ex)
            {
                //myCustomLogger.LogException(ex);
                MessageBox.Show($"Fooking error mate.\n{ex.Message}", "Error when Downloading", MessageBoxButton.OK);
            }
            return null;
        }
        public async Task<IEnumerable<Spell>?> GetAllAsync()
        {
            var unfilteredData = await GetData();
            if (unfilteredData == null || unfilteredData.Count <= 0) return null;
            List<Spell> result = new List<Spell>();
            foreach (Open5eSpellExportable spell in unfilteredData.Results)
            {
                result.Add(spell.TransformToInternalModel());
            }
            return result;
        }
    }
}