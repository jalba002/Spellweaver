namespace Spellweaver.Data
{
    public interface IDBProvider<T> where T : class
    {
        T GetInstance { get; }

        Action<T>? OnDatabaseLoaded { get; set; }
        Task<T?> GetAllAsync();
    }
}