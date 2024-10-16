﻿using Spellweaver.Backend;
using Spellweaver.Data;
using Spellweaver.Data.Models;

namespace Spellweaver.Services.Backend
{
    internal class SpellweaverEAI : BaseExporterAndImporter
    {
        List<Spell> mySpells = new List<Spell>();

        public SpellweaverEAI(Serializer serializer) : base(serializer)
        {
            // Serializer for the objecttt
        }

        #region Import
        protected override List<Spell> TransformStringToSpellList(string data)
        {
            try
            {
                var spellbook = _serializer.Deserialize<List<Spell>>(data);
                if (spellbook == null) throw new NullReferenceException();
                return spellbook;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"Error when importing using {nameof(BookOfSpellsEAI)}. Message: {ex.Message}");
                return new List<Spell>();
            }
        }
        #endregion

        #region Export
        protected override void TransformToCustomList(List<Spell> spells)
        {
            mySpells = spells;
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
        #endregion
    }
}
