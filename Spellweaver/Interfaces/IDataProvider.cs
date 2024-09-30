namespace Spellweaver.Interfaces
{
    public interface IDataProvider<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}