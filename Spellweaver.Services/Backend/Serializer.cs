using System.Text.Json;

namespace Spellweaver.Backend
{
    public class Serializer
    {
        public static T? Deserialize<T>(string data, JsonSerializerOptions serializerOptions)
        {
            return JsonSerializer.Deserialize<T>(data, serializerOptions);
        }

        public static string Serialize<T>(T data, JsonSerializerOptions serializerOptions)
        {
            return JsonSerializer.Serialize(data, serializerOptions);
        }
    }
}
