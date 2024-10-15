using Spellweaver.Data;

namespace Spellweaver.Services.Backend;
public interface IExporterAndImporter
{
    string Export(List<Spell> spells);
    List<Spell> Import(string data);
}

public class BaseExporterAndImporter : IExporterAndImporter
{
    public string Export(List<Spell> spells)
    {
        throw new NotImplementedException();
    }

    public List<Spell> Import(string data)
    {
        throw new NotImplementedException();
    }
}
