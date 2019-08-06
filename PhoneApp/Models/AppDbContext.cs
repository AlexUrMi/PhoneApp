using Microsoft.EntityFrameworkCore;

namespace PhoneApp.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(eb =>
            {
                eb.Property(b => b.Price).HasColumnType("decimal(18,2)");
            });
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }


    }
}
