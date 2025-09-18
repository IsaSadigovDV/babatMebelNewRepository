using BabatMebel.App.Context;
using BabatMebel.App.Entities.BaseEntities;
using BabatMebel.App.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BabatMebel.App.Repository.Concretes
{
    public class ReadRepository<TModel, TContext> : IReadRepository<TModel> where TModel : BaseEntity
        where TContext : AppDbContext
    {
        private readonly TContext _context;

        public ReadRepository(TContext context)
        {
            _context = context;
        }

        public DbSet<TModel> Table => _context.Set<TModel>();

        public IQueryable<TModel> FilterAll(Expression<Func<TModel, bool>> filter)
        {
            return Table.Where(filter);
        }

        public async Task<TModel> FilterFirstAsync(Expression<Func<TModel, bool>> filter)
        {
            return await Table.FirstOrDefaultAsync(filter);
        }

        public IQueryable<TModel> GetAll() => Table;

        public Task<TModel> GetByIdAsync(Guid id)
        {
            return Table.FirstOrDefaultAsync(m => m.Id == id);
        }
    }

    public class ReadRepository<TModel> : ReadRepository<TModel, AppDbContext> where TModel : BaseEntity
    {
        public ReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
