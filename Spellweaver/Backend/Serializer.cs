using System.Text.Json;

namespace Spellweaver.Backend
{
    public class Serializer
    {
        public static T? Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
