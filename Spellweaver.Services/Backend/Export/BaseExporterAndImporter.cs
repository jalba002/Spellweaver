﻿using Spellweaver.Backend;
using Spellweaver.Data;

namespace Spellweaver.Services.Backend;
public interface IExporterAndImporter
{
    string Export(List<Spell> spells);
    List<Spell> Import(string data);
}

public class BaseExporterAndImporter : IExporterAndImporter
{
    protected string returnResult = "";
    protected Serializer _serializer;

    public BaseExporterAndImporter(Serializer serializer)
    {
        _serializer = serializer;
    }

    public string Export(List<Spell> spells)
    {
        returnResult = "";
        TransformToCustomList(spells);
        TransformToCustomTable();
        return TransformToString();
    }

    public List<Spell> Import(string data)
    {
        throw new NotImplementedException();
    }

    // Receive List of Spells
    // Transform to list of spells formatted
    // Add then to the table format that the web desires
    // Transform it to a string formatted, ready to be stored as a JSON
    // Return it as string to whoever wants to save it
    protected virtual void TransformToCustomList(List<Spell> spells)
    {
        // Transform to the custom spell list used to export!
    }
    protected virtual void TransformToCustomTable()
    {
        // Transform to the custom table or spellbook used to export!
    }
    protected virtual string TransformToString()
    {
        // Transform to string
        // use the resurnResult yes or yes, otherwise oof.
        return returnResult;
    }
}
