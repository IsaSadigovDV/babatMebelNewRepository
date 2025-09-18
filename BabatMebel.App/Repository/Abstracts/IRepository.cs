using BabatMebel.App.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Repository.Abstracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
