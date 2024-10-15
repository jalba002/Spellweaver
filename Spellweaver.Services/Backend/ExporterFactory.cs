using Serilog;

namespace Spellweaver.Services.Backend;
public class ExporterFactory
{
    // This class should have Exporters registered
    // When an exportation format is needed its Exporter is returned
    // Exporters should have Import and Export come from an Interface
    // And they should receive a List of Spell, and return a List of Spell

    public enum DataType
    {
        Spellweaver,
        Open5eWebsite,
        SpellbookApp,
        BookOfSpells // This is the one that Ferry uses.
    }

    private Dictionary<DataType, IExporterAndImporter> _factories = new();
    public ExporterFactory()
    {
        // Can we just create 4 default ones?

    }

    public void RegisterFactory<T>(DataType dataType) where T : IExporterAndImporter
    {
        if (_factories.ContainsKey(dataType))
        {
            Log.Warning($"Exportation factory already contains an exporter of type {dataType.ToString()}. Addition is ignored");
            return;
        }

        IExporterAndImporter instance = Activator.CreateInstance(typeof(T)) as IExporterAndImporter;
        _factories.Add(dataType, instance);
    }

    public void UnregisterFactory(DataType dataType)
    {
        // remove from dictionary if existing one is there already.
        if (_factories.ContainsKey(dataType))
        {
            _factories.Remove(dataType);
        }
        else
        {
            Log.Warning($"Tried removing an unexisting {nameof(IExporterAndImporter)} of type {dataType.ToString()}");
        }
    }
}