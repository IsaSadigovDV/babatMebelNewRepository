using Microsoft.AspNetCore.Identity;

namespace BabatMebel.App.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Basket> Baskets { get; set; } = new List<Basket>();
    }
}
