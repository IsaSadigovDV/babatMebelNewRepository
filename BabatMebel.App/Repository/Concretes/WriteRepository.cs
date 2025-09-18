using BabatMebel.App.Context;
using BabatMebel.App.Entities.BaseEntities;
using BabatMebel.App.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Repository.Concretes
{
    public class WriteRepository<TModel, TContext> : IWriteRepository<TModel> where TModel : BaseEntity where TContext : AppDbContext
    {
        private readonly TContext _context;

        public WriteRepository(TContext context)
        {
            _context = context;
        }

        public DbSet<TModel> Table => _context.Set<TModel>();

        public async Task<bool> AddAsync(TModel entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(IEnumerable<TModel> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(TModel entity)
        {
            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);

            if (entity == null)
                return false;

            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool SoftRemove(TModel entity)
        {
            entity.IsDeleted = true;
            return Update(entity);
        }

        public async Task<bool> SoftRemoveByIdAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);

            if (entity == null)
                return false;

            return SoftRemove(entity);
        }

        public bool Update(TModel entity)
        {
            var entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }

    public class WriteRepository<TModel> : WriteRepository<TModel, AppDbContext> where TModel : BaseEntity
    {
        public WriteRepository(AppDbContext context) : base(context)
        {
        }
    }

}
