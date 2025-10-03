using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftoftheGiversFoundation.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}