using Newtonsoft.Json;
namespace Spellweaver.Backend
{
    public class Serializer
    {
        public static dynamic? Deserialize(string data)
        {
            var result = JsonConvert.DeserializeObject(data) as dynamic;
            return result;
        }
    }
}
