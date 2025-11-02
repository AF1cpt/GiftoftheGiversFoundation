using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftGivers.Models
{
    // This model represents a single task a volunteer can sign up for.
    public class VolunteerTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Task")]
        public string TaskDescription { get; set; }

        [Required]
        public string Status { get; set; } // e.g., "Open", "Assigned"

        [Required]
        [Display(Name = "Associated Disaster")]
        public int DisasterId { get; set; }

        // Navigation Property - Made nullable
        [ForeignKey("DisasterId")]
        public virtual Disaster? Disaster { get; set; }

        // --- FIX ---
        // Added VolunteerId (as nullable) to link to the user who signed up
        // This fixes the "does not contain a definition for 'VolunteerId'" error
        public string? VolunteerId { get; set; } // Nullable, as it's empty when "Open"

        // Navigation Property - Made nullable
        [ForeignKey("VolunteerId")]
        public virtual IdentityUser? Volunteer { get; set; }
        // -----------

        // Constructor to set default values and fix CS8618
        public VolunteerTask()
        {
            Status = "Open";
            // Set default for non-nullable string to fix CS8618
            TaskDescription = string.Empty;
        }
    }
}

