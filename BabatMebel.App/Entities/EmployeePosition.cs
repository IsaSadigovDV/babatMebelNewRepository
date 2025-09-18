using BabatMebel.App.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabatMebel.App.Entities
{
    public class EmployeePosition : BaseEntity
    {
        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey(nameof(Position))]
        public Guid PositionId { get; set; }
        public Position Position { get; set; }
    }
}
