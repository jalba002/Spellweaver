using System.Globalization;
using System.Text.Json.Serialization;

namespace Spellweaver.Data
{
    public class O5ESpellModel : BaseSpellModel
    {
        public O5ESpellModel() { }
        public O5ESpellModel(Spell original) : base(original)
        {
            this.TransformInternalToCustomExportable(original);
        }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("desc")]
        public string? Desc { get; set; }

        [JsonPropertyName("higher_level")]
        public string? HigherLevel { get; set; }

        [JsonPropertyName("page")]
        public string? Page { get; set; }

        [JsonPropertyName("range")]
        public string? Range { get; set; }

        [JsonPropertyName("target_range_sort")]
        public int? TargetRangeSort { get; set; }

        [JsonPropertyName("components")]
        public string? Components { get; set; }

        [JsonPropertyName("requires_verbal_components")]
        public bool RequiresVerbalComponents { get; set; }

        [JsonPropertyName("requires_somatic_components")]
        public bool RequiresSomaticComponents { get; set; }

        [JsonPropertyName("requires_material_components")]
        public bool RequiresMaterialComponents { get; set; }

        [JsonPropertyName("material")]
        public string? Material { get; set; }

        [JsonPropertyName("can_be_cast_as_ritual")]
        public bool CanBeCastAsRitual { get; set; }

        [JsonPropertyName("ritual")]
        public string? Ritual { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("concentration")]
        public string? Concentration { get; set; }

        [JsonPropertyName("requires_concentration")]
        public bool RequiresConcentration { get; set; }

        [JsonPropertyName("casting_time")]
        public string? CastingTime { get; set; }

        [JsonPropertyName("level")]
        public string? Level { get; set; }

        [JsonPropertyName("level_int")]
        public int? LevelInt { get; set; }

        [JsonPropertyName("spell_level")]
        public int? SpellLevel { get; set; }

        [JsonPropertyName("school")]
        public string? School { get; set; }

        [JsonPropertyName("dnd_class")]
        public string? DndClass { get; set; }

        [JsonPropertyName("spell_lists")]
        public string?[] SpellLists { get; set; }

        [JsonPropertyName("archetype")]
        public string? Archetype { get; set; }

        [JsonPropertyName("circles")]
        public string? Circles { get; set; }

        [JsonPropertyName("document__slug")]
        public string? DocumentSlug { get; set; }

        [JsonPropertyName("document__title")]
        public string? DocumentTitle { get; set; }

        [JsonPropertyName("document__license_url")]
        public Uri? DocumentLicenseUrl { get; set; }

        [JsonPropertyName("document__url")]
        public Uri? DocumentUrl { get; set; }

        //Here we prepare the item to gather info for the app. 
        // This is not to export, but to get the int?ernal data.
        public override Spell TransformToInternalModel()
        {
            Spell spell = new Spell()
            {
                Name = this.Name,
                Level = this.LevelInt.ToString(),
                School = string.Concat(this.School[0].ToString().ToUpper(CultureInfo.CurrentCulture), this.School.AsSpan(1)),
                CastingTime = this.CastingTime,
                Range = this.Range,
                IsVocal = RequiresVerbalComponents,
                IsSomatic = RequiresSomaticComponents,
                IsMaterial = RequiresMaterialComponents,
                DescriptionMaterials = this.Material,
                Duration = this.Duration,
                Classes = this.DndClass,
                Description = this.Desc,
                Source = this.DocumentTitle,
                IsRitual = this.CanBeCastAsRitual,
                IsConcentration = this.RequiresConcentration
            };

            return spell;
        }

        public override void TransformInternalToCustomExportable(Spell? original)
        {
            // Well fuck that.
            // Not doing it yet...
        }
    }
}