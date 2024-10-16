using Spellweaver.Data;

namespace Spellweaver.Services.Backend
{
    internal class Open5eWebsiteEAI : BaseExporterAndImporter
    {
        List<O5ESpellModel> mySpells = new List<O5ESpellModel>();

        public Open5eWebsiteEAI(Serializer serializer) : base(serializer)
        {
            // Serializer for the objecttt
        }

        protected override void TransformToCustomList(List<Spell> spells)
        {
            foreach (Spell spell in spells)
            {
                mySpells.Add(new O5ESpellModel(spell));
            }
        }
        protected override void TransformToCustomTable()
        {
            // Generate a table or a Spellbook?
        }
        protected override string TransformToString()
        {
            // Modify the returnString variable
            return _serializer.Serialize(mySpells);
        }
    }
}
