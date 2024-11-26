namespace Spellweaver.Data
{
    public class SpellbookModel
    {
        // Spellbook model for containing the needed data.
        List<Spell> cantripSpells = new List<Spell>();
        List<Spell> levelOneSpells = new List<Spell>();
        List<Spell> levelTwoSpells = new List<Spell>();
        List<Spell> levelThreeSpells = new List<Spell>();
        List<Spell> levelFourSpells = new List<Spell>();
        List<Spell> levelFiveSpells = new List<Spell>();
        List<Spell> levelSixSpells = new List<Spell>();
        List<Spell> levelSevenSpells = new List<Spell>();
        List<Spell> levelEightSpells = new List<Spell>();
        List<Spell> levelNineSpells = new List<Spell>();
        List<Spell> otherSpells = new List<Spell>();

        #region API
        public void AddSpellToList(Spell spell)
        {
            AddSpell(spell);
        }

        public void RemoveSpellFromList(Spell spell)
        {
            RemoveSpell(spell);
        }
        #endregion

        #region Private
        private void AddSpell(Spell spell)
        {
            bool IsNumber = Int32.TryParse(spell.Level, out int spellLevelToInt);

            if (!IsNumber) return;

            switch (spellLevelToInt)
            {
                case 0: cantripSpells.Add(spell); break;
                case 1: levelOneSpells.Add(spell); break;
                case 2: levelTwoSpells.Add(spell); break;
                case 3: levelThreeSpells.Add(spell); break;
                case 4: levelFourSpells.Add(spell); break;
                case 5: levelFiveSpells.Add(spell); break;
                case 6: levelSixSpells.Add(spell); break;
                case 7: levelSevenSpells.Add(spell); break;
                case 8: levelEightSpells.Add(spell); break;
                case 9: levelNineSpells.Add(spell); break;
                default: otherSpells.Add(spell); break;
            }
        }

        private void RemoveSpell(Spell spell)
        {
            bool IsNumber = Int32.TryParse(spell.Level, out int spellLevelToInt);

            if (!IsNumber) return;

            switch (spellLevelToInt)
            {
                case 0: cantripSpells.Remove(spell); break;
                case 1: levelOneSpells.Remove(spell); break;
                case 2: levelTwoSpells.Remove(spell); break;
                case 3: levelThreeSpells.Remove(spell); break;
                case 4: levelFourSpells.Remove(spell); break;
                case 5: levelFiveSpells.Remove(spell); break;
                case 6: levelSixSpells.Remove(spell); break;
                case 7: levelSevenSpells.Remove(spell); break;
                case 8: levelEightSpells.Remove(spell); break;
                case 9: levelNineSpells.Remove(spell); break;
                default: otherSpells.Remove(spell); break;
            }
        }
        #endregion
    }
}