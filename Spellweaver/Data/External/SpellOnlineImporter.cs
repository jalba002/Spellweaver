using System.Net.Http;

namespace Spellweaver.Backend
{
    public class SpellOnlineImporter
    {
        private static HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.open5e.com")
        };
        public static async Task<string> GetAllAsync()
        {
            using HttpResponseMessage response = await _client.GetAsync("/v1/spells/?format=json&page=1");
            //Console.WriteLine(response.EnsureSuccessStatusCode());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Write it on a textfile then we check.
            //var result = Deserializer.Deserialize(jsonResponse);
            // deserialize as a s
            return jsonResponse;
        }
    }
}