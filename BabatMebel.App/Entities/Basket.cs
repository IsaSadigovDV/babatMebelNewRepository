using BabatMebel.App.Entities.BaseEntities;

namespace BabatMebel.App.Entities
{
    public class Basket : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
