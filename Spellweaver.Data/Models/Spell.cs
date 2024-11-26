using System.Runtime.CompilerServices;

namespace Spellweaver.Data
{
    // This is my base model class. We use this as a template for everything else.
    // This is a basic DND 5e Spell. Maybe we can split it more...
    // If we wanted to choose other RPGs we could implement interface-oriented models.
    // For example an spell could have description, a name, a custom level too.
    // Maybe the max level is not the same as others...
    public class Spell : BaseSpellModel, ICloneable
    {
        public string? Name { get; set; } = string.Empty;
        public string? School { get; set; } = string.Empty;
        public string? Level { get; set; } = string.Empty;
        public string? CastingTime { get; set; } = string.Empty;
        public string? Target { get; set; } = string.Empty;
        public string? Range { get; set; } = string.Empty;
        public string? Duration { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? DescriptionMaterials { get; set; } = string.Empty;
        public string? UpcastDescription { get; set; } = string.Empty;
        public string? Source { get; set; } = string.Empty;
        public string? Classes { get; set; } = string.Empty;
        public bool IsConcentration { get; set; } = false;
        public bool IsRitual { get; set; } = false;
        public bool IsVocal { get; set; } = false;
        public bool IsSomatic { get; set; } = false;
        public bool IsMaterial { get; set; } = false;

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

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
