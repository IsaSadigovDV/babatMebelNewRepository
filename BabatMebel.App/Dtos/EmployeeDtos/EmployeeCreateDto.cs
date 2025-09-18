using System.ComponentModel.DataAnnotations;

namespace BabatMebel.App.Dtos.EmployeeDtos
{
    public class EmployeeCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        //relations
        public ICollection<Guid> PositionIds { get; set; } = new List<Guid>();
    }
}
