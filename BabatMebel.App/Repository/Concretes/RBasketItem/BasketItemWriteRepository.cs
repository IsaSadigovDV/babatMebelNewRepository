using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RBasketItem;

namespace BabatMebel.App.Repository.Concretes.RBasketItem
{
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
