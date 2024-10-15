using System.Text.Json;

namespace Spellweaver.Backend
{
    public class Serializer
    {
        private readonly JsonSerializerOptions defaultOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        public T? Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, defaultOptions);
        }

        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, defaultOptions);
        }
    }
}
