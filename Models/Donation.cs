using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System; // Required for DateTime

namespace GiftGivers.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // Initialized

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; } = null!; // Mark as non-null

        [Required]
        public int DisasterId { get; set; }

        [ForeignKey("DisasterId")]
        public virtual Disaster Disaster { get; set; } = null!; // Mark as non-null

        [Required]
        public string ItemType { get; set; } = string.Empty; // Initialized

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
    }
}