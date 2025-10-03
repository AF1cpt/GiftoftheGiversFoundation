using Microsoft.EntityFrameworkCore;

namespace GiftoftheGiversFoundation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-Many: Lecturer → Claims
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Lecturer)
                .WithMany(l => l.Claims)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-One: Claim → Coordinator
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Coordinator)
                .WithMany(co => co.ReviewedClaims)
                .HasForeignKey(c => c.CoordinatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
