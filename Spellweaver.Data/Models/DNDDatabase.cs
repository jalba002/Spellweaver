namespace Spellweaver.Data
{
    public abstract class DNDDatabase
    {
        // here we have a model that has Spells, Classes, and other default BS.
        protected List<Spell>? Spells { get; set; }
        protected List<School>? Schools { get; set;}
        //public List<Source> Sources { get; set; }
        protected List<Level>? Levels { get; set; }
        protected List<CastingTime>? CastingTimes { get; set; }

        public abstract Task<List<Spell>> GetSpellsAsync();
        public abstract Task<List<School>> GetSchoolsAsync();
        public abstract Task<List<Level>> GetLevelsAsync();
        public abstract Task<List<CastingTime>> GetCastingTimesAsync();
    }
}