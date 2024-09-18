using Spellweaver.ViewModel;

namespace Spellweaver.Managers
{
    public class SpellManager
    {
        private SpellItemViewModel? _currentSpell;
        public SpellItemViewModel? CurrentSpell
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
