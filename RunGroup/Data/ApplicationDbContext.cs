using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunGroup.Models;
using Microsoft.EntityFrameworkCore;
namespace RunGroup.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
