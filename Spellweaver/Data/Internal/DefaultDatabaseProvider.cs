using Spellweaver.Model;

namespace Spellweaver.Data
{
    public class DefaultDatabaseProvider : IDBProvider<DNDDatabase>
    {
        private DNDDatabase? _database;

        public async Task<DNDDatabase?> GetAllAsync()
        {
            // Load everything into the DND database and return it.
            if (_database != null) return _database;

            _database = new DNDDatabase();
            await Task.Delay(10);

            // Load default stuff into the DB.
            _database.Spells = (List<Spell>?)await new SpellDataProvider().GetAllAsync();
            _database.Schools = (List<School>?)await new SchoolDataProvider().GetAllAsync();
            _database.CastingTimes = (List<CastingTime>?)await new CastingTimeDataProvider().GetAllAsync();
            _database.Levels = (List<Level>?)await new LevelDataProvider().GetAllAsync();

            // The SERVER delay LEMAO.
            await Task.Delay(10);
            return _database;
        }
    }
}