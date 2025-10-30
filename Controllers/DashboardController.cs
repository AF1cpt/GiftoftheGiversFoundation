using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var userId = currentUser.Id;
            var viewModel = new DashboardViewModel
            {
                UserDonations = await _context.Donations
                    .Where(d => d.UserId == userId)
                    .Include(d => d.Disaster)
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync(),
                UserTasks = await _context.VolunteerTasks
                    .Where(v => v.UserId == userId)
                    .Include(v => v.Disaster)
                    .OrderByDescending(v => v.TaskId)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}