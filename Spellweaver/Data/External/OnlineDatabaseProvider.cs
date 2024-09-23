using Spellweaver.Backend;
using Spellweaver.Model;

namespace Spellweaver.Data
{
    public class OnlineDatabaseProvider : DNDDatabase, IDBProvider<DNDDatabase>
    {
        private bool IsDatabaseLoaded = false;
        public DNDDatabase GetInstance => this;

        public Action<DNDDatabase>? OnDatabaseLoaded { get; set; }

        public async Task<DNDDatabase?> GetAllAsync()
        {
            if (IsDatabaseLoaded) return this;
            // Use the online db provider to supply spells from outside.
            Spells = await new Open5eSpellDataProvider().GetAllAsync() as List<Spell>;
            Schools = await new SchoolDataProvider().GetAllAsync() as List<School>;
            CastingTimes = await new CastingTimeDataProvider().GetAllAsync() as List<CastingTime>;
            Levels = await new LevelDataProvider().GetAllAsync() as List<Level>;

            OnDatabaseLoaded?.Invoke(this);
            IsDatabaseLoaded = true;
            return this;
        }
    }
}