namespace Spellweaver.Data.Models;

public class BookOfSpellsExportStructure
{
    public BookOfSpellsExportStructure() { }
    public BookOfSpellsExportStructure(List<Spell> spells) 
    {
        Spells = new();
        Characters = Array.Empty<object>();
        foreach (Spell spell in spells)
        {
            Spells.Add(new BookOfSpellsSpellModel(spell));
        }
    }
    public List<BookOfSpellsSpellModel>? Spells { get; set; }
    public object[]? Characters { get; set; }
}
