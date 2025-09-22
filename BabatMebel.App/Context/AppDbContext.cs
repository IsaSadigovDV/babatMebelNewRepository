using BabatMebel.App.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        // dbset -> databasaya set ele yeni ki ef core vasitesile sql-e sorgu gonderirik ve db set elediyimiz classlar sql terefde table olurlar
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    }
}
