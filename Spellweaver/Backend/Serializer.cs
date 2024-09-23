using Newtonsoft.Json;
namespace Spellweaver.Backend
{
    public class Serializer
    {
        public static T? Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
