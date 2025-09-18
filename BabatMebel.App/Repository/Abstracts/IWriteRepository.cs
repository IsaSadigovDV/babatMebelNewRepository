using BabatMebel.App.Entities.BaseEntities;

namespace BabatMebel.App.Repository.Abstracts
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        bool Remove(T entity);
        bool SoftRemove(T entity);
        Task<bool> RemoveByIdAsync(Guid id);
        Task<bool> SoftRemoveByIdAsync(Guid id);
        bool Update(T entity);
        Task SaveAsync();
    }
}
