using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RPosition;

namespace BabatMebel.App.Repository.Concretes.RPosition
{
    public class PositionReadRepository : ReadRepository<Position>, IPositonReadRepository
    {
        public PositionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
