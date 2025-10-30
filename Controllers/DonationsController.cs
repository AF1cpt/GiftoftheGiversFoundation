using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    [Authorize]
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge(); // Fix for possible null

            var applicationDbContext = _context.Donations
                                               .Include(d => d.Disaster)
                                               .Where(d => d.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["DisasterId"] = new SelectList(_context.Disasters, "DisasterId", "DisasterName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,DisasterId,ItemType,Quantity")] Donation donation)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge(); // Fix for possible null

            donation.UserId = user.Id;
            donation.DonationDate = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thank you, your donation has been recorded!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisasterId"] = new SelectList(_context.Disasters, "DisasterId", "DisasterName", donation.DisasterId);
            return View(donation);
        }
    }
}