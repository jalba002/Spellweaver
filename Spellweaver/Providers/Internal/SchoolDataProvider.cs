using Spellweaver.Data;
using Spellweaver.Interfaces;

namespace Spellweaver.Providers
{
    class SchoolDataProvider : IDataProvider<School>
    {
        public async Task<IEnumerable<School>?> GetAllAsync()
        {
            return new List<School>
            {
                new School() { Name = "Abjuration" },
                new School() { Name = "Conjuration" },
                new School() { Name = "Divination" },
                new School() { Name = "Enchantment" },
                new School() { Name = "Evocation" },
                new School() { Name = "Illusion" },
                new School() { Name = "Necromancy" },
                new School() { Name = "Transmutation" }
            };
        }
    }
}
