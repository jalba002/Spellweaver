namespace Spellweaver.Backend.Interfaces
{
    public interface IDataImporter
    {
        public Task<T?> GetData<T>() where T : class;
    }
}