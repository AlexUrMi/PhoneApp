using Microsoft.EntityFrameworkCore;

namespace PhoneApp.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }


    }
}
