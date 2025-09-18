using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.REmployee;

namespace BabatMebel.App.Repository.Concretes.REmployee
{
    public class EmployeeWriteRepository : WriteRepository<Employee>, IEmployeeWriteRepository
    {
        public EmployeeWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
