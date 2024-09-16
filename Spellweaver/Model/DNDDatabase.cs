namespace Spellweaver.Model
{
    public class DNDDatabase
    {
        // here we have a model that has Spells, Classes, and other default BS.
        public List<Spell>? Spells { get; set; }
        public List<School>? Schools { get; set;}
        //public List<Source> Sources { get; set; }
        public List<Level>? Levels { get; set; }
        public List<CastingTime>? CastingTimes { get; set; }
    }
}