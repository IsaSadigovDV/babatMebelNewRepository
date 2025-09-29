using BabatMebel.App.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BabatMebel.App.Entities
{
    public class Furniture : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 10000)]
        public double Price { get; set; }

        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
