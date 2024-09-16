using Spellweaver.Model;

namespace Spellweaver.Data
{
    public class CastingTimeDataProvider : IDataProvider<CastingTime>
    {
        public async Task<IEnumerable<CastingTime>?> GetAllAsync()
        {
            await Task.Delay(100);
            // Simulate a server connection?
            return new List<CastingTime>
            {
                new CastingTime { Name = "Free action" },
                new CastingTime { Name = "1 bonus action" },
                new CastingTime { Name = "1 action" },
                new CastingTime { Name = "1 reaction" },
                new CastingTime { Name = "1 minute" },
                new CastingTime { Name = "1 hour" },
                new CastingTime { Name = "8 hours" },
                new CastingTime { Name = "24 hours" },
            };
        }
    }
}