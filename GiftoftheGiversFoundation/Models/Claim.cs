using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftoftheGiversFoundation.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int? CoordinatorId { get; set; }
        public Coordinator Coordinator { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 200)]
        public int HoursWorked { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal HourlyRate { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        public string? Feedback { get; set; }
    }
}