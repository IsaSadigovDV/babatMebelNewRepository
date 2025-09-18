namespace BabatMebel.App.Dtos.EmployeeDtos
{
    public class EmployeeUpdateDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }

        //relations
        public ICollection<Guid>? PositionIds { get; set; } = new List<Guid>();
    }
}
