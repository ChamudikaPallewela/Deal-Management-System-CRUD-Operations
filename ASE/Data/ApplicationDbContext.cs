using Microsoft.EntityFrameworkCore;
using ASE.Models;
namespace ASE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Deal> Deals { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deal>()
                .HasMany(d => d.Hotels)
                .WithOne(h => h.Deal)
                .HasForeignKey(h => h.DealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
