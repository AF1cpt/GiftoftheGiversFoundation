using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftGivers.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Donation Date")]
        public DateTime DonationDate { get; set; }

        [Required(ErrorMessage = "Please select a disaster.")]
        [Display(Name = "Associated Disaster")]
        public int DisasterId { get; set; }

        // Navigation Property - Made nullable
        [ForeignKey("DisasterId")]
        public virtual Disaster? Disaster { get; set; }

        // --- FIX ---
        // Added UserId to link to the user who made the donation
        [Required]
        public string UserId { get; set; }

        // Navigation Property - Made nullable
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
        // -----------

        public Donation()
        {
            DonationDate = DateTime.Now;
            // Set defaults for non-nullable strings to fix CS8618
            ItemType = string.Empty;
            UserId = string.Empty;
        }
    }
}

