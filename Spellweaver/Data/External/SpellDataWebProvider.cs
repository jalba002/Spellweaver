using Spellweaver.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Spellweaver.Data
{
    class SpellDataWebProvider : IDataProvider<Spell>
    {
        private static HttpClient _client = new()
        {
            BaseAddress = new Uri("https://api.open5e.com")
        };

        public async Task<IEnumerable<Spell>?> GetAllAsync()
        {
            // Await an HTTPS response
            //var response = await https
            // Parse result
            //var json = await _client.GetAsync("https://api.open5e.com/v1/spells/");
            var requestResult = GetAsync();
            await Task.Delay(1000);
            Console.Write(requestResult);

            return new List<Spell>
            {
                new Spell() {Name="Test Spell", Level = "0"}
            };
        }

        public async Task<string> GetAsync()
        {
            using HttpResponseMessage response = await _client.GetAsync("/v1/spells/");

            //Console.WriteLine(response.EnsureSuccessStatusCode());

            var jsonResponse = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"{jsonResponse}\n");
            return jsonResponse;
        }
    }
}
