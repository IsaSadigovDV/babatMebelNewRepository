using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RBasket;

namespace BabatMebel.App.Repository.Concretes.RBasket
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
