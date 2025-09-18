using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.REmployee;

namespace BabatMebel.App.Repository.Concretes.REmployee
{
    public class EmployeeReadRepository : ReadRepository<Employee>, IEmployeeReadRepository
    {
        public EmployeeReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
