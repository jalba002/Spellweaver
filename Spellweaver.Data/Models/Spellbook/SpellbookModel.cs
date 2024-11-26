namespace Spellweaver.Data
{
    public class SpellbookModel
    {
        //
        List<Spell> AllSpells = new List<Spell>();

        // Spellbook model for containing the needed data.
        List<Spell> cantripSpells => AllSpells.Where(x => x.Level == "Cantrip").ToList();
        List<Spell> levelOneSpells => AllSpells.Where(x => x.Level == "1").ToList();
        List<Spell> levelTwoSpells => AllSpells.Where(x => x.Level == "2").ToList();
        List<Spell> levelThreeSpells => AllSpells.Where(x => x.Level == "3").ToList();
        List<Spell> levelFourSpells => AllSpells.Where(x => x.Level == "4").ToList();
        List<Spell> levelFiveSpells => AllSpells.Where(x => x.Level == "5").ToList();
        List<Spell> levelSixSpells => AllSpells.Where(x => x.Level == "6").ToList();
        List<Spell> levelSevenSpells => AllSpells.Where(x => x.Level == "7").ToList();
        List<Spell> levelEightSpells => AllSpells.Where(x => x.Level == "8").ToList();
        List<Spell> levelNineSpells => AllSpells.Where(x => x.Level == "9").ToList();
        List<Spell> otherSpells => AllSpells.Where(x => Int32.TryParse(x.Level, out int num) && num >= 10).ToList();

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