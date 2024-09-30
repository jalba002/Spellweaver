namespace Spellweaver.Providers
{
    public interface IDataProvider<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}