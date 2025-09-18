using BabatMebel.App.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BabatMebel.App.Entities
{
    public class Setting : BaseEntity
    {
        public string Icon { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
    }
}
