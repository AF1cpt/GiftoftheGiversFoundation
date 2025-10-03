using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftoftheGiversFoundation.Models
{
    // Represents a reported disaster incident.
    public class Disaster
    {
        [Key]
        public int DisasterId { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Disaster Name")]
        public string DisasterName { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        // Navigation properties for related entities
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<VolunteerTask> VolunteerTasks { get; set; } = new List<VolunteerTask>();
    }
}
