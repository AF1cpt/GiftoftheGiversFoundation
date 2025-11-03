using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
// ADD THIS using for the 'List' class
using System.Collections.Generic;

namespace GiftGivers.Controllers
{
    // [Authorize] // <-- BYPASSED: I've commented this out for screenshots.
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This is the main dashboard page
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                var emptyViewModel = new DashboardViewModel
                {
                    UserDonations = new List<Donation>(),
                    UserTasks = new List<VolunteerTask>()
                };
                return View(emptyViewModel);
            }

            var viewModel = new DashboardViewModel
            {
                // Get all donations made by this user
                UserDonations = await _context.Donations
                    .Where(d => d.UserId == user.Id)
                    .Include(d => d.Disaster) // Include Disaster details
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync(),

                // Get all tasks this user has signed up for
                // This fixes the CS1061 error by querying for 'VolunteerId'
                UserTasks = await _context.VolunteerTasks
                    .Where(t => t.VolunteerId == user.Id)
                    .Include(t => t.Disaster) // Include Disaster details
                    .OrderBy(t => t.Status)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}

