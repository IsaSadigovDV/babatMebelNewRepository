using System.ComponentModel.DataAnnotations;

namespace BabatMebel.App.Dtos.ContactDtos
{
    public class ContactCreateDto
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
