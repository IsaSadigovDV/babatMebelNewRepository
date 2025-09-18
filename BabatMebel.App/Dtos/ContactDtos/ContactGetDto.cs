namespace BabatMebel.App.Dtos.ContactDtos
{
    public class ContactGetDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
