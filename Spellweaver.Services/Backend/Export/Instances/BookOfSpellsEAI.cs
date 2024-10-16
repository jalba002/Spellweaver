using Spellweaver.Data;
using Spellweaver.Data.Models;

namespace Spellweaver.Services.Backend
{
    internal class BookOfSpellsEAI : BaseExporterAndImporter
    {
        List<Spell> mySpells = new List<Spell>();
        BookOfSpellsExportStructure spellbook;

        public BookOfSpellsEAI(Serializer serializer) : base(serializer)
        {
            // Serializer for the objecttt
        }

        #region Import
        protected override List<Spell> TransformStringToSpellList(string data)
        {
            Serilog.Log.Debug($"Importing spells using {nameof(BookOfSpellsEAI)}");
            try
            {
                var spellbook = _serializer.Deserialize<BookOfSpellsExportStructure>(data);
                Serilog.Log.Debug($"Importing {spellbook.Spells.Count} spells using {nameof(BookOfSpellsEAI)}");
                mySpells.Clear();
                foreach (var item in spellbook.Spells)
                {
                    mySpells.Add(item.TransformToInternalModel());
                }
                return mySpells;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"Error when importing using {nameof(BookOfSpellsEAI)}. Message: {ex.Message}");
                return new List<Spell>();
            }
            finally
            {
                mySpells.Clear();
            }
        }
        #endregion

        #region Export
        protected override void TransformToCustomList(List<Spell> spells)
        {
            mySpells.AddRange(spells);
        }
        protected override void TransformToCustomTable()
        {
            // Generate a table or a Spellbook?
            spellbook = new BookOfSpellsExportStructure(mySpells);
        }
        protected override string TransformToString()
        {
            // Modify the returnString variable
            return _serializer.Serialize(spellbook);
        }
        #endregion
    }
}
