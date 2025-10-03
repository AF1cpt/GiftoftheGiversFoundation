using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftoftheGiversFoundation.Models
{
    public class Coordinator
    {
        [Key]
        public int CoordinatorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Claim> ReviewedClaims { get; set; } = new List<Claim>();
    }
}
