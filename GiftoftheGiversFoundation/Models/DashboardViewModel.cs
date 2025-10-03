using System.Collections.Generic;

namespace GiftGivers.Models
{
    // A special model to pass multiple data lists to the Dashboard view.
    public class DashboardViewModel
    {
        public IEnumerable<Donation> UserDonations { get; set; }
        public IEnumerable<VolunteerTask> UserVolunteerTasks { get; set; }

        public DashboardViewModel()
        {
            UserDonations = new List<Donation>();
            UserVolunteerTasks = new List<VolunteerTask>();
        }
    }
}
