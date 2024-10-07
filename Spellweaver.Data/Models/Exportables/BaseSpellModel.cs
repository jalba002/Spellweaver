namespace Spellweaver.Data
{
    public abstract class BaseSpellModel
    {
        public BaseSpellModel() { }
        public BaseSpellModel(Spell? original) { }
        public abstract void TransformInternalToCustomExportable(Spell original);
        public abstract Spell TransformToInternalModel();
    }
}