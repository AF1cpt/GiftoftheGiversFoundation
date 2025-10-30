using System.Collections.Generic;

namespace GiftGivers.Models
{
    public class DashboardViewModel
    {
        public List<Donation> UserDonations { get; set; } = new List<Donation>();
        public List<VolunteerTask> UserTasks { get; set; } = new List<VolunteerTask>();
    }
}
