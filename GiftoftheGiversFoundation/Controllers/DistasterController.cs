using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiftoftheGiversFoundation.Data;
using GiftoftheGiversFoundation.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GiftoftheGiversFoundation.Controllers
{
    [Authorize]
    public class DisasterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Disaster
        // Displays a list of all disasters.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disasters.OrderByDescending(d => d.StartDate).ToListAsync());
        }

        // GET: /Disaster/Details/5
        // Shows details for a specific disaster, including related donations and tasks.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var disaster = await _context.Disasters
                .Include(d => d.Donations)
                .Include(d => d.VolunteerTasks)
                .FirstOrDefaultAsync(m => m.DisasterId == id);

            if (disaster == null) return NotFound();

            return View(disaster);
        }

        // GET: /Disaster/Create
        // Shows the form to report a new disaster.
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Disaster/Create
        // Handles the submission of the new disaster form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisasterName,Location,StartDate,EndDate,Description")] Disaster disaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disaster);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Disaster reported successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(disaster);
        }
    }
}
