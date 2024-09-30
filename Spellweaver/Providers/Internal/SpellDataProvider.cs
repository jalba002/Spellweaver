using Spellweaver.Data;

namespace Spellweaver.Providers
{
    class SpellDataProvider : IDataProvider<Spell>
    {
        public async Task<IEnumerable<Spell>?> GetAllAsync()
        {
            return new List<Spell>
            {
                new Spell()
                {
                    Name = "Mage Hand",
                    Level = "0",
                    School="Conjuration",
                    CastingTime = "1 action",
                    Range = "30 feet",
                    Target = "A point you choose within range",
                    IsVocal = true,
                    IsSomatic=true,
                    IsMaterial=false,
                    Duration="1 minute",
                    Classes="Bard, Sorcerer, Warlock, Wizard",
                    Description="A spectral, floating hand appears at a point you choose within range. The hand lasts for the duration or until you dismiss it as an action. The hand vanishes if it is ever more than 30 feet away from you or if you cast this spell again.\r\nYou can use your action to control the hand. You can use the hand to manipulate an object, open an unlocked door or container, stow or retrieve an item from an open container, or pour the contents out of a vial. You can move the hand up to 30 feet each time you use it.\r\nThe hand can’t attack, activate magic items, or carry more than 10 pounds.",
                    Source="PHB",
                    IsRitual=false,
                    IsConcentration=false
                },
                new Spell()
                {
                    Name = "Bless",
                    Level = "1",
                    School = "Enchantment",
                    CastingTime = "1 action",
                    Range = "30 feet",
                    Target = "Up to three creatures of your choice within range",
                    IsVocal = true,
                    IsSomatic = true,
                    IsMaterial = true,
                    DescriptionMaterials = "A sprinkling of holy water",
                    Duration = "Up to 1 minute",
                    Classes = "Cleric, Paladin",
                    Description = "You bless up to three creatures of your choice within range. Whenever a target makes an attack roll or a saving throw before the spell ends, the target can roll a d4 and add the number rolled to the attack roll or saving throw.",
                    Source = "PHB",
                    IsRitual = false,
                    IsConcentration = true,
                    UpcastDescription = "When you cast this spell using a spell slot of 2nd level or higher, you can target one additional creature for each slot level above 1st."
                },
                new Spell()
                {
                    Name = "Recordatori",
                    Description = "Per carregar totes les dades default que la app necessita, pots fer-ho" +
                    "amb una suma de SchoolDataProvider i demés, creant un DNDDatabase (com la app original), i que hi hagi" +
                    "una versió offline i una online. La offline carrega School, Casting Times i Levels " +
                    "La online ho treura de la 5e Open. Ja veurem com ho fem. Primer esa mierda."
                }
            };
        }
    }
}
