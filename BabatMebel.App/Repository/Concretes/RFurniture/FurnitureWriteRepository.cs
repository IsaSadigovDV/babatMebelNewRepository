using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RFurniture;

namespace BabatMebel.App.Repository.Concretes.RFurniture
{
    public class FurnitureWriteRepository : WriteRepository<Furniture>, IFurnitureWriteRepository
    {
        public FurnitureWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
