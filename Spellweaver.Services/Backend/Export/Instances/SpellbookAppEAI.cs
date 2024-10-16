using Spellweaver.Backend;
using Spellweaver.Data;

namespace Spellweaver.Services.Backend
{
    internal class SpellbookAppEAI : BaseExporterAndImporter
    {
        List<Spell> mySpells = new List<Spell>();
        MobileAppExportStructure spellbook;

        public SpellbookAppEAI(Serializer serializer) : base(serializer)
        {
            // Serializer for the objecttt
        }

        protected override void TransformToCustomList(List<Spell> spells)
        {
            mySpells.AddRange(spells);
        }
        protected override void TransformToCustomTable()
        {
            // Generate a table or a Spellbook?
            spellbook = new MobileAppExportStructure(mySpells);
        }
        protected override string TransformToString()
        {
            // Modify the returnString variable
            return _serializer.Serialize(spellbook);
        }
    }
}
