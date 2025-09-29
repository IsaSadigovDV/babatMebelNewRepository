using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RBasket;

namespace BabatMebel.App.Repository.Concretes.RBasket
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
