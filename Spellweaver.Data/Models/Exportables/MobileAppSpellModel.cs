using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Spellweaver.Data
{
    public class MobileAppSpellModel : BaseSpellModel
    {
        // We can use this as "interpreter" to deserialize everything.
        // It has a method to take a spell and the all properties managed to deserialize for each value.
        public MobileAppSpellModel(Spell? original) : base(original)
        {
            TransformInternalToCustomExportable(original);
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("school")]
        public string? School { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("casting_time")]
        public string? CastingTime { get; set; }

        [JsonPropertyName("range")]
        public string? Range { get; set; }

        [JsonPropertyName("components")]
        public string? Components { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("description_high")]
        public string? DescriptionHigh { get; set; }

        [JsonPropertyName("book")]
        public string? Book { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("classes")]
        public string? Classes { get; set; }

        [JsonPropertyName("concentration")]
        public bool Concentration { get; set; }

        [JsonPropertyName("ritual")]
        public bool Ritual { get; set; }

        [JsonPropertyName("sound")]
        public string? Sound { get; set; }

        public override Spell TransformToInternalModel()
        {
            Spell spell = new Spell();
            if (Components != null)
            {
                spell.Name = this.Name;
                spell.Level = this.Level.ToString();
                spell.School = this.School;
                spell.CastingTime = this.CastingTime;
                spell.Range = this.Range;
                spell.Duration = this.Duration;
                spell.Classes = this.Classes;
                spell.Description = this.Description;
                spell.Source = this.Book;
                spell.IsRitual = this.Ritual;
                spell.IsConcentration = this.Concentration;
                spell.UpcastDescription = this.DescriptionHigh;
                spell.IsVocal = Components.Contains('V');
                spell.IsSomatic = Components.Contains('S');
                spell.IsMaterial = Components.Contains('M');
                if (spell.IsMaterial)
                {
                    spell.DescriptionMaterials = Regex.Match(Components, "(?<=\\()[^)]*(?=\\))").Value;
                }
            }
            return spell;
        }

        public override void TransformInternalToCustomExportable(Spell? original)
        {
            if (original is null) return;
            Components =
                (original.IsVocal ? "V, " : "") +
            (original.IsSomatic ? "S, " : "") +
                (original.IsMaterial ? "M (" + original.DescriptionMaterials + ")" : "");

            Name = original.Name;
            Id = 0;
            if (int.TryParse(original.Level, out int lvl))
            {
                Level = lvl;
                if (lvl == 0)
                {
                    Level = -1;
                }
            }
            else
            {
                Level = 0;
            }
            School = original.School;
            CastingTime = original.CastingTime;
            Range = original.Range;
            Duration = original.Duration;
            Classes = original.Classes;
            Description = original.Description;
            DescriptionHigh = original.UpcastDescription;
            Book = original.Source;
            Ritual = original.IsRitual;
            Concentration = original.IsConcentration;
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}