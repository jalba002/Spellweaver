using System.Runtime.InteropServices.Marshalling;

namespace Spellweaver.Data
{
    public abstract class BaseSpellModel : ICloneable
    {
        public BaseSpellModel() { }
        public BaseSpellModel(Spell original) { }
        public abstract object Clone();
        public abstract void TransformInternalToCustomExportable(Spell original);
        public abstract Spell TransformToInternalModel();
    }
}