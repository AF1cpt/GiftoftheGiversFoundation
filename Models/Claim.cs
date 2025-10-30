using System.ComponentModel.DataAnnotations;

namespace GiftGivers.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }
        public string Name { get; set; } = string.Empty; // Initialized
        public string Description { get; set; } = string.Empty; // Initialized
    }
}