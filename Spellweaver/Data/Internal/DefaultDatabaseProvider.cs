using Spellweaver.Model;

namespace Spellweaver.Data
{
    public class DefaultDatabaseProvider : IDBProvider<DNDDatabase>
    {
        public async Task<DNDDatabase?> GetAllAsync()
        {
            // Load everything into the DND database and return it.
            var db = new DNDDatabase();
            await Task.Delay(10);

            // Load default shit into the DB.
            db.Spells = (List<Spell>?)await new SpellDataProvider().GetAllAsync();
            db.Schools = (List<School>?)await new SchoolDataProvider().GetAllAsync();
            db.CastingTimes = (List<CastingTime>?)await new CastingTimeDataProvider().GetAllAsync();
            db.Levels = (List<Level>?)await new LevelDataProvider().GetAllAsync();

            // The SERVER delay LEMAO.
            await Task.Delay(10);
            return db;
        }
    }
}