using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RFurniture;

namespace BabatMebel.App.Repository.Concretes.RFurniture
{
    public class FurnitureReadRepository : ReadRepository<Furniture>, IFurnitureReadRepository
    {
        public FurnitureReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
