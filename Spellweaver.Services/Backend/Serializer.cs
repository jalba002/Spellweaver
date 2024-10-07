using System.Text.Json;

namespace Spellweaver.Backend
{
    public class Serializer
    {
        private static JsonSerializerOptions defaultOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        public static T? Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, defaultOptions);
        }

        public static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, defaultOptions);
        }
    }
}
