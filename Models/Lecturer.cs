using System.ComponentModel.DataAnnotations;

namespace GiftGivers.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }
        public string Name { get; set; } = string.Empty; // Initialized
        public string Email { get; set; } = string.Empty; // Initialized
    }
}