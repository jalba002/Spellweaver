using Spellweaver.Data;
using Spellweaver.ViewModel;
using System.Collections.ObjectModel;

namespace Spellweaver.Managers
{
    public static class SpellManager
    {
        private static SpellItemViewModel? _currentSpell;
        public static SpellItemViewModel? CurrentSpell
        {
            get
            {
                return _currentSpell;
            }
            set
            {
                _currentSpell = value;
            }
        }

        private static ObservableCollection<SpellItemViewModel> _spellList = new();
        public static ObservableCollection<SpellItemViewModel> SpellList
        {
            get { return _spellList; }
            private set
            {
                _spellList = value;
            }
        }

        public static void SetSpellList(List<Spell> spellList)
        {
            if (spellList == null || spellList.Count <= 0) return;
            SpellList.Clear();
            foreach (Spell spell in spellList)
            {
                AddToSpellList(new SpellItemViewModel(spell));
            }
        }

        public static void SetSpellList(ObservableCollection<SpellItemViewModel> spellList)
        {
            if (spellList == null) return;
            SpellList = spellList;
        }

        public static void AddToSpellList(SpellItemViewModel singleSpell)
        {
            if (singleSpell == null) return;
            SpellList.Add(singleSpell);
        }

        private static void AddToSpellListAtIndex(SpellItemViewModel singleSpell, int index)
        {
            if (singleSpell == null) return;
            SpellList.Insert(index, singleSpell);
        }
        
        public static void AddToSpellListAfterSelectedSpell(SpellItemViewModel singleSpell)
        {
            if (singleSpell == null || CurrentSpell == null) return;
            SpellList.Insert(SpellList.IndexOf(CurrentSpell)+1, singleSpell);
        }

        public static void RemoveSpellFromList(SpellItemViewModel singleSpell)
        {
            if (singleSpell == null) return;
            if (!SpellList.Contains(singleSpell)) return;

            SpellList.Remove(singleSpell);
        }
    }
}
