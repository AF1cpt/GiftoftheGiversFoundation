using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization; // Import this!
using Microsoft.AspNetCore.Identity; // Import this!
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    [Authorize] // This entire controller is now secure, fulfilling POE Feature 1
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Donations/
        // Shows a list of the *current user's* donations
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userDonations = await _context.Donations
                .Where(d => d.UserId == currentUser.Id) // Only get donations for this user
                .Include(d => d.Disaster) // Include the related Disaster data
                .ToListAsync();

            return View(userDonations);
        }

        // GET: /Donations/Create
        // This is the "Make a Donation" form
        public async Task<IActionResult> Create()
        {
            // We need to pass a list of disasters to the view for the dropdown
            ViewBag.DisasterId = new SelectList(
                await _context.Disasters.ToListAsync(),
                "DisasterId",
                "DisasterName"
            );
            return View();
        }

        // POST: /Donations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemType,Quantity,DisasterId")] Donation donation)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                // Assign the current user's ID and the current date
                donation.UserId = currentUser.Id;
                donation.DonationDate = DateTime.Now;

                _context.Add(donation);
                await _context.SaveChangesAsync();

                // Redirect to the user's list of donations
                return RedirectToAction(nameof(Index));
            }

            // If the model is invalid, re-populate the dropdown and show the form again
            ViewBag.DisasterId = new SelectList(
                await _context.Disasters.ToListAsync(),
                "DisasterId",
                "DisasterName",
                donation.DisasterId
            );
            return View(donation);
        }
    }
}
