using BabatMebel.App.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabatMebel.App.Entities
{
    public class BasketItem : BaseEntity
    {
        [ForeignKey(nameof(Furniture))] public Guid FurnitureId { get; set; }
        public Furniture Furniture { get; set; }

        [ForeignKey(nameof(Basket))] public Guid BasketId { get; set; }
        public Basket Basket { get; set; }

        public int ItemCount { get; set; }
    }
}
