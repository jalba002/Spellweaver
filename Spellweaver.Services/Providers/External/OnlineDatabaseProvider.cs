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

        public override async Task<List<Spell>> GetALLSpellsAsync()
        {
            var result = await new O5ESpellDataProvider().GetAllDatabase();
            return result.ToList();
        }

        public override async Task<List<Spell>> GetAllSpellsThatMatch(string match)
        {
            var result = await new O5ESpellDataProvider().GetSpellMatch(match);
            List<Spell> spells = new List<Spell>();
            // Return an empty
            if (result == null || result?.Results.Length <= 0) return spells;

            foreach (var item in result.Results)
            {
                spells.Add(item.TransformToInternalModel());
            }
            return spells;
        }
    }
}