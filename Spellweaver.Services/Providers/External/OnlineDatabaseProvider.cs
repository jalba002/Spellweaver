using Spellweaver.Backend;
using Spellweaver.Data;
using Spellweaver.Services;

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
            var result = await new O5ESpellDataProvider().GetAllAsync();
            return result.ToList();
        }

        public async Task<List<Spell>> GetALLSpellsAsync()
        {
            var result = await new O5ESpellDataProvider().GetAllDatabase();
            return result.ToList();
        }
    }
}