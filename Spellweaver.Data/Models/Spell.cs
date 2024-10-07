namespace Spellweaver.Data
{
    // This is my base model class. We use this as a template for everything else.
    // This is a basic DND 5e Spell. Maybe we can split it more...
    // If we wanted to choose other RPGs we could implement interface-oriented models.
    // For example an spell could have description, a name, a custom level too.
    // Maybe the max level is not the same as others...
    public class Spell : BaseSpellModel
    {
        public string? Name { get; set; }
        public string? School { get; set; }
        public string? Level { get; set; }
        public string? CastingTime { get; set; }
        public string? Target { get; set; }
        public string? Range { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? DescriptionMaterials { get; set; }
        public string? UpcastDescription { get; set; }
        public string? Source { get; set; }
        public string? Classes { get; set; }
        public bool IsConcentration { get; set; }
        public bool IsRitual { get; set; }
        public bool IsVocal { get; set; }
        public bool IsSomatic { get; set; }
        public bool IsMaterial { get; set; }

        public Spell() : base()
        {

        }

        public Spell(Spell? original) : base(original)
        {
            // nothing. Because its the original. duh.
            // But structure...
        }

        public override Spell TransformToInternalModel()
        {
            return this;
        }

        public override void TransformInternalToCustomExportable(Spell original)
        {
            // Nothing, this is already the same spell?
        }
    }
}
