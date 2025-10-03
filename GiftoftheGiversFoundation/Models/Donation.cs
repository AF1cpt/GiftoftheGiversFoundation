using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GiftoftheGiversFoundation.Models
{
    // Represents a resource donation made by a user.
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        [Required]
        [Display(Name = "Associated Disaster")]
        public int DisasterId { get; set; }
        [ForeignKey("DisasterId")]
        public virtual Disaster Disaster { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Donation")]
        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
    }
}
