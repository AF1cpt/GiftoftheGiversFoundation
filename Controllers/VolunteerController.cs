using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization; // Import this!
using Microsoft.AspNetCore.Identity; // Import this!
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    [Authorize] // This entire controller is also secure
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VolunteerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Volunteer/
        // Lists all "Open" tasks
        public async Task<IActionResult> Index()
        {
            var openTasks = await _context.VolunteerTasks
                .Where(t => t.Status == "Open")
                .Include(t => t.Disaster)
                .ToListAsync();

            return View(openTasks);
        }

        // POST: /Volunteer/SignUp/5
        // This action signs up a user for a task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.VolunteerTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Update the task
            task.Status = "Assigned";
            task.VolunteerId = currentUser.Id; // Assign the task to the current user

            _context.Update(task);
            await _context.SaveChangesAsync();

            // Redirect to a "My Tasks" page (which we can build next)
            // For now, let's redirect back to the list of open tasks
            return RedirectToAction(nameof(Index));
        }

        // GET: /Volunteer/MyTasks
        // (Optional but good) Shows tasks the user has signed up for
        public async Task<IActionResult> MyTasks()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var myTasks = await _context.VolunteerTasks
                .Where(t => t.VolunteerId == currentUser.Id)
                .Include(t => t.Disaster)
                .ToListAsync();

            return View(myTasks);
        }
    }
}
