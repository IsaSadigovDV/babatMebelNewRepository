using BabatMebel.App.Entities.BaseEntities;

namespace BabatMebel.App.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        //relations
        public ICollection<EmployeePosition> Positions { get; set; } = new List<EmployeePosition>();
    }
}
