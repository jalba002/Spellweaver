using Spellweaver.Model;

namespace Spellweaver.Data
{
    public class DefaultDatabaseProvider : DNDDatabase, IDBProvider<DNDDatabase>
    {
        public DNDDatabase GetInstance => this;

        public Action<DNDDatabase>? OnDatabaseLoaded { get; set; }

        public async Task<DNDDatabase?> GetAllAsync()
        {
            // We should load the spells here then.


            // Load everything into the DND database and return it.
            // We return the default database in case it is already loaded.
            // Better way to do this?

            // Load default stuff into the DB.
            Spells = (List<Spell>?)await new SpellDataProvider().GetAllAsync();
            Schools = (List<School>?)await new SchoolDataProvider().GetAllAsync();
            CastingTimes = (List<CastingTime>?)await new CastingTimeDataProvider().GetAllAsync();
            Levels = (List<Level>?)await new LevelDataProvider().GetAllAsync();

            OnDatabaseLoaded?.Invoke(this);
            return this;
        }
    }
}