using Serilog;
using Spellweaver.Backend;
using Spellweaver.Data;

namespace Spellweaver.Services.Backend;
public class ExporterFactory
{
    // This class should have Exporters registered
    // When an exportation format is needed its Exporter is returned
    // Exporters should have Import and Export come from an Interface
    // And they should receive a List of Spell, and return a List of Spell

    public enum ExportType
    {
        Spellweaver,
        Open5eWebsite,
        SpellbookApp,
        BookOfSpells // This is the one that Ferry uses.
    }

    private Dictionary<ExportType, IExporterAndImporter> _factories = new();
    private Serializer _serializer;
    private bool _isBusy;
    public bool IsBusy
    {
        get
        {
            return _isBusy;
        }
        set
        {
            _isBusy = value;
            // notify?
        }
    }

    public ExporterFactory(Serializer serializer)
    {
        // Can we just create 4 default ones?
        _serializer = serializer;

        InstantiateDefaultFactories();
    }

    private void InstantiateDefaultFactories()
    {
        RegisterFactory(ExportType.Spellweaver, new SpellweaverEAI(_serializer));
        RegisterFactory(ExportType.Open5eWebsite, new Open5eWebsiteEAI(_serializer));
        RegisterFactory(ExportType.SpellbookApp, new SpellbookAppEAI(_serializer));
        RegisterFactory(ExportType.BookOfSpells, new BookOfSpellsEAI(_serializer));
    }

    #region Registration
    public void RegisterFactory<T>(ExportType dataType) where T : IExporterAndImporter
    {
        if (_factories.ContainsKey(dataType))
        {
            Log.Warning($"Exportation factory already contains an exporter of type {dataType.ToString()}. Addition is ignored");
            return;
        }

        IExporterAndImporter instance = Activator.CreateInstance(typeof(T)) as IExporterAndImporter;
        _factories.Add(dataType, instance);
    }

    public void RegisterFactory(ExportType dataType, IExporterAndImporter exporter)
    {
        if (_factories.ContainsKey(dataType))
        {
            Log.Warning($"Exportation factory already contains an exporter of type {dataType.ToString()}. Addition is ignored");
            return;
        }

        _factories.Add(dataType, exporter);
    }

    public void UnregisterFactory(ExportType dataType)
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
    #endregion

    #region Methods
    public string? Export(List<Spell> spells, ExportType dataType)
    {
        if (IsBusy) return null;
        try
        {
            IsBusy = true;
            // Get the thing here.
            // Recover item from factory.
            // Tell it to export it
            // Return string.
            // ...
            // SUCCESS!
            _factories.TryGetValue(dataType, out IExporterAndImporter exporterModule);
            return exporterModule.Export(spells);
        }
        catch (Exception ex)
        {
            // Return an error if needed.
            Log.Error($"Error when Importing through the ExporterFactory. Error: {ex.Message}");
            return null;
        }
        finally
        {
            IsBusy = false;
        }
    }

    public List<Spell>? Import(string data, ExportType dataType)
    {
        // Data is already read by a file reader or else.
        if (IsBusy) return null;
        try
        {
            IsBusy = true;
            _factories.TryGetValue(dataType, out IExporterAndImporter exporterModule);
            return exporterModule.Import(data);
        }
        catch (Exception ex)
        {
            // Log or something
            Log.Error($"Error when Importing through the ExporterFactory. Error: {ex.Message}");
            return null;
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Getters
    public ExportType[] GetAvailableExporters()
    {
        return _factories.Keys.ToArray();
    }
    #endregion
}