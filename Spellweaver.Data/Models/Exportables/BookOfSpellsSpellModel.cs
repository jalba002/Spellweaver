using System.Text.Json.Serialization;

namespace Spellweaver.Data.Models;

public class BookOfSpellsSpellModel : BaseSpellModel
{
    public BookOfSpellsSpellModel() { }
    public BookOfSpellsSpellModel(int? id, string? uuid, string? name, string? description, int? level, string? school, string? castingTime, string? range, string? duration, int? ritual, string? book, string? attackRoll, Components? components, string? classes)
    {
        Id = id;
        Uuid = uuid;
        Name = name;
        Description = description;
        Level = level;
        School = school;
        CastingTime = castingTime;
        Range = range;
        Duration = duration;
        Ritual = ritual;
        Book = book;
        AttackRoll = attackRoll;
        Components = components;
        Classes = classes;
    }

    public BookOfSpellsSpellModel (Spell spell)
    {
        this.TransformInternalToCustomExportable(spell);
    }

    public int? Id { get; set; }
    public string? Uuid { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Level { get; set; }
    public string? School { get; set; }
    public string? CastingTime { get; set; }
    public string? Range { get; set; }
    public string? Duration { get; set; }
    public int? Ritual { get; set; }
    public string? Book { get; set; }
    public string? AttackRoll { get; set; }
    public Components? Components { get; set; }
    public string? Classes { get; set; }

    public override object Clone()
    {
        return this.MemberwiseClone();
    }

    public override void TransformInternalToCustomExportable(Spell original)
    {
        this.Id = 10000010;
        this.Name = original.Name;
        this.Description = original.Description;
        if (Int32.TryParse(original.Level, out var newLevel))
        {
            this.Level = newLevel;
        }
        this.School = original.School;
        this.CastingTime = original.CastingTime;
        this.Range = original.Range;
        this.Duration = original.Duration;
        this.Ritual = original.IsRitual ? 1 : 0;
        this.Book = original.Source;

        string vocal = original.IsVocal ? "V" : "";
        string somatic = original.IsSomatic ? "S" : "";
        string material = original.IsMaterial ? "M" : "";

        this.Components = new Components()
        {
            components = string.Concat(vocal, somatic, material),
            materials = original.IsMaterial ? original.DescriptionMaterials : ""
        };

        this.Classes = original.Classes;
    }

    public override Spell TransformToInternalModel()
    {
        throw new NotImplementedException();
    }
}

public class Components
{
    [JsonPropertyName("Components")]
    public string components { get; set; }
    [JsonPropertyName("Materials")]
    public string materials { get; set; }
}