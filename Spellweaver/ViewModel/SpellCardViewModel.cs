using Spellweaver.Managers;

namespace Spellweaver.ViewModel
{
    public class SpellCardViewModel : ViewModelBase
    {
        public SpellCardViewModel()
        {

        }

        public SpellItemViewModel? CurrentSpell
        {
            get { return SpellManager.CurrentSpell; }
        }
    }
}
