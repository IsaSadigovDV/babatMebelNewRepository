using BabatMebel.App.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BabatMebel.App.Entities
{
    public class Service : BaseEntity
    {
        public string Icon { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
