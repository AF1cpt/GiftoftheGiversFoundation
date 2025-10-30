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
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VolunteerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VolunteerTasks.Include(v => v.Disaster).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(int id)
        {
            var task = await _context.VolunteerTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge(); // Fix for possible null

            task.UserId = user.Id;
            task.Status = "In Progress";

            _context.Update(task);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "You have successfully signed up for the task!";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}