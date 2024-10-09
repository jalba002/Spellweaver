using Spellweaver.Data;
using Spellweaver.Managers;
using System.Security.Cryptography.X509Certificates;

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
