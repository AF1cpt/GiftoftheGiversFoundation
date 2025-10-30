using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftGivers.Models
{
    public class VolunteerTask
    {
        [Key]
        public int TaskId { get; set; }

        public string TaskDescription { get; set; } = string.Empty; // Initialized
        public string Status { get; set; } = "Open"; // Initialized

        public int DisasterId { get; set; }
        [ForeignKey("DisasterId")]
        public virtual Disaster Disaster { get; set; } = null!; // Mark as non-null

        public string? UserId { get; set; } // Made nullable
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; } // Made nullable
    }
}