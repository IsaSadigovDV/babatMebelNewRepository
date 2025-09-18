namespace BabatMebel.App.Dtos.EmployeeDtos
{
    public class EmployeeGetDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        public List<string> Positions { get; set; }
    }
}
