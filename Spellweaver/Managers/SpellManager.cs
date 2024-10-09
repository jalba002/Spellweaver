using Spellweaver.ViewModel;

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
    }
}
