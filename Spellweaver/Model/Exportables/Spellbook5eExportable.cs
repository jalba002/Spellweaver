using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Spellweaver.Model.Exportables
{
    public class Spellbook5ThEdition
    {
        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("db")]
        public long? Db { get; set; }

        [JsonProperty("data")]
        public List<Spellbook5eExportable> Data { get; set; }
    }

    public class Spellbook5eExportable : ExportableModel
    {
        // We can use this as "interpreter" to deserialize everything.
        // It has a method to take a spell and the all properties managed to deserialize for each value.
        public Spellbook5eExportable(Spell? original) : base(original)
        {
            TransformInternalToCustomExportable(original);
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("school")]
        public string? School { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("casting_time")]
        public string? CastingTime { get; set; }

        [JsonProperty("range")]
        public string? Range { get; set; }

        [JsonProperty("components")]
        public string? Components { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("description_high")]
        public string? DescriptionHigh { get; set; }

        [JsonProperty("book")]
        public string? Book { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [JsonProperty("classes")]
        public string? Classes { get; set; }

        [JsonProperty("concentration")]
        public bool Concentration { get; set; }

        [JsonProperty("ritual")]
        public bool Ritual { get; set; }

        [JsonProperty("sound")]
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
                if(lvl == 0)
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
    }
}
