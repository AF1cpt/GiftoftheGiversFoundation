using System.ComponentModel.DataAnnotations;

namespace GiftGivers.Models
{
    public class Coordinator
    {
        [Key]
        public int CoordinatorId { get; set; }
        public string Name { get; set; } = string.Empty; // Initialized
        public string Email { get; set; } = string.Empty; // Initialized
    }
}