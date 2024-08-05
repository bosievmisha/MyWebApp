namespace RunGroup.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<T>> GetByCity(string city);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
