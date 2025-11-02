using System.Collections.Generic;

namespace GiftGivers.Models
{
    // This model holds all the data needed for the dashboard
    public class DashboardViewModel
    {
        public List<Donation> UserDonations { get; set; }
        public List<VolunteerTask> UserTasks { get; set; }

        public DashboardViewModel()
        {
            UserDonations = new List<Donation>();
            UserTasks = new List<VolunteerTask>();
        }
    }
}
