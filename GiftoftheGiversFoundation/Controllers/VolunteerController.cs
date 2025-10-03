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

        // GET: /Volunteer
        // Shows a list of all available volunteer tasks.
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.VolunteerTasks.Include(v => v.Disaster).ToListAsync();
            return View(tasks);
        }

        // POST: /Volunteer/SignUp/5
        // Allows a user to sign up for a specific, open task.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(int id)
        {
            var task = await _context.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();

            if (!string.IsNullOrEmpty(task.UserId))
            {
                TempData["ErrorMessage"] = "This task has already been assigned.";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await _userManager.GetUserAsync(User);
            task.UserId = currentUser.Id;
            task.Status = "In Progress";

            _context.Update(task);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "You have successfully signed up for the task!";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
