using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiftoftheGiversFoundation.Data;
using GiftoftheGiversFoundation.Models;

namespace GiftoftheGiversFoundation.Controllers
{
    [Authorize] // Ensures only logged-in users can access this controller.
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Dashboard
        // Serves as the main landing page for authenticated users, showing a summary of their activity.
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var userDonations = await _context.Donations
                .Include(d => d.Disaster)
                .Where(d => d.UserId == currentUser.Id)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();

            var userTasks = await _context.VolunteerTasks
                .Include(t => t.Disaster)
                .Where(t => t.UserId == currentUser.Id)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                UserDonations = userDonations,
                UserVolunteerTasks = userTasks
            };

            return View(viewModel);
        }
    }
}
