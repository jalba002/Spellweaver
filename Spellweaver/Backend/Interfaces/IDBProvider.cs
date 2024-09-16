namespace Spellweaver.Data
{
    public interface IDBProvider<T> where T : class
    {
        Task<T?> GetAllAsync();
    }
}