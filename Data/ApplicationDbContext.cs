using GiftGivers.Models; // Use the correct namespace
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftGivers.Data
{
    // Must inherit from IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add your models here
        public DbSet<Disaster> Disasters { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
    }
}