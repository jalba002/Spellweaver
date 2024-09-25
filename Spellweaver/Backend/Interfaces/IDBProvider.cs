namespace Spellweaver.Data
{
    public interface IDBProvider<T> where T : class
    {
        Action<T>? OnDatabaseLoaded { get; set; }
        Task<T?> GetAllAsync();
    }
}