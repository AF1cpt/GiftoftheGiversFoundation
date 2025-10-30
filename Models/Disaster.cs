using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftGivers.Models
{
    public class Disaster
    {
        [Key]
        public int DisasterId { get; set; }

        [Required]
        public string DisasterName { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        // Initialize collections to prevent null reference errors
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<VolunteerTask> VolunteerTasks { get; set; } = new List<VolunteerTask>();
    }
}

