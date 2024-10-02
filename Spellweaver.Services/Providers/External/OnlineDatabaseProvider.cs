using Spellweaver.Backend;
using Spellweaver.Data;

namespace Spellweaver.Providers
{
    public class OnlineDatabaseProvider : DNDDatabase
    {
        public override async Task<List<CastingTime>> GetCastingTimesAsync()
        {
            var result = await new CastingTimeDataProvider().GetAllAsync();
            return result.ToList();
        }

        public override async Task<List<Level>> GetLevelsAsync()
        {
            var result = await new LevelDataProvider().GetAllAsync();
            return result.ToList();
        }

        public override async Task<List<School>> GetSchoolsAsync()
        {
            var result = await new SchoolDataProvider().GetAllAsync();
            return result.ToList();
        }

        public override async Task<List<Spell>> GetSpellsAsync()
        {
            var result = await new Open5eSpellDataProvider().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<Spell>> GetALLSpellsAsync()
        {
            var result = await new Open5eSpellDataProvider().GetAllDatabase();
            return result.ToList();
        }
    }
}