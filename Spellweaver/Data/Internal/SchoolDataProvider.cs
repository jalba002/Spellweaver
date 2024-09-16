using Spellweaver.Model;

namespace Spellweaver.Data
{
    class SchoolDataProvider : IDataProvider<School>
    {
        public async Task<IEnumerable<School>?> GetAllAsync()
        {
            await Task.Delay(25);
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
