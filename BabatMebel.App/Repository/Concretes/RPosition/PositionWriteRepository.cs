
using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RPosition;

namespace BabatMebel.App.Repository.Concretes.RPosition
{
    public class PositionWriteRepository : WriteRepository<Position>, IPositionWriteRepository
    {
        public PositionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
