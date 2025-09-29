using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RBasketItem;

namespace BabatMebel.App.Repository.Concretes.RBasketItem
{
    public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
