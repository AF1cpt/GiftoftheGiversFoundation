using GiftGivers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftGivers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add these lines to register your models with the database
        public DbSet<Disaster> Disasters { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
    }
}
