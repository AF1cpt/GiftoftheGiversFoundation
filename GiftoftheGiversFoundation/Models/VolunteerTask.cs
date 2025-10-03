using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GiftoftheGiversFoundation.Models
{
    // Represents a task that a volunteer can sign up for.
    public class VolunteerTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int DisasterId { get; set; }
        [ForeignKey("DisasterId")] a
        public virtual Disaster Disaster { get; set; }

        // Nullable because a task may not be assigned to a user initially.
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Open"; // Default status
    }
}
