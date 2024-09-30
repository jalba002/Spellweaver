using Spellweaver.Data;

namespace Spellweaver.Providers
{
    public class CastingTimeDataProvider : IDataProvider<CastingTime>
    {
        public async Task<IEnumerable<CastingTime>> GetAllAsync()
        {
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