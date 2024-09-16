using Newtonsoft.Json;

namespace Spellweaver.Model.Exportables
{
    public class Open5eSpellExportable : ExportableModel
    {
        public Open5eSpellExportable(Spell? original) : base(original)
        {
            TransformInternalToCustomExportable(original);
        }

        [JsonProperty("slug")]
        public string? Slug { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("desc")]
        public string? Desc { get; set; }

        [JsonProperty("higher_level")]
        public string? HigherLevel { get; set; }

        [JsonProperty("page")]
        public string? Page { get; set; }

        [JsonProperty("range")]
        public string? Range { get; set; }

        [JsonProperty("target_range_sort")]
        public int? TargetRangeSort { get; set; }

        [JsonProperty("components")]
        public string? Components { get; set; }

        [JsonProperty("requires_verbal_components")]
        public bool RequiresVerbalComponents { get; set; }

        [JsonProperty("requires_somatic_components")]
        public bool RequiresSomaticComponents { get; set; }

        [JsonProperty("requires_material_components")]
        public bool RequiresMaterialComponents { get; set; }

        [JsonProperty("material")]
        public string? Material { get; set; }

        [JsonProperty("can_be_cast_as_ritual")]
        public bool CanBeCastAsRitual { get; set; }

        [JsonProperty("ritual")]
        public string? Ritual { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("concentration")]
        public string? Concentration { get; set; }

        [JsonProperty("requires_concentration")]
        public bool RequiresConcentration { get; set; }

        [JsonProperty("casting_time")]
        public string? CastingTime { get; set; }

        [JsonProperty("level")]
        public string? Level { get; set; }

        [JsonProperty("level_int")]
        public int? LevelInt { get; set; }

        [JsonProperty("spell_level")]
        public int? SpellLevel { get; set; }

        [JsonProperty("school")]
        public string? School { get; set; }

        [JsonProperty("dnd_class")]
        public string? DndClass { get; set; }

        [JsonProperty("spell_lists")]
        public string?[] SpellLists { get; set; }

        [JsonProperty("archetype")]
        public string? Archetype { get; set; }

        [JsonProperty("circles")]
        public string? Circles { get; set; }

        [JsonProperty("document__slug")]
        public string? DocumentSlug { get; set; }

        [JsonProperty("document__title")]
        public string? DocumentTitle { get; set; }

        [JsonProperty("document__license_url")]
        public Uri DocumentLicenseUrl { get; set; }

        [JsonProperty("document__url")]
        public Uri DocumentUrl { get; set; }

        //Here we prepare the item to gather info for the app. 
        // This is not to export, but to get the int?ernal data.
        public override Spell TransformToInternalModel()
        {
            Spell spell = new Spell()
            {
                Name = this.Name,
                Level = this.LevelInt.ToString(),
                School = string.Concat(this.School[0].ToString().ToUpper(), this.School.AsSpan(1)),
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