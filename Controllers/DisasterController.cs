using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    // We are using "DisasterController" (singular)
    // This will match your URL: .../Disaster/Create
    public class DisasterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Disaster
        // This is the "View Active Disasters" page
        public async Task<IActionResult> Index()
        {
            var disasters = await _context.Disasters.ToListAsync();

            // MAJOR FIX: We are explicitly telling the controller
            // to find the view in the "Views/Disasters" (plural) folder.
            return View("~/Views/Disasters/Index.cshtml", disasters);
        }

        // GET: /Disaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disaster = await _context.Disasters
                .Include(d => d.Donations)
                .Include(d => d.VolunteerTasks)
                .FirstOrDefaultAsync(m => m.DisasterId == id);

            if (disaster == null)
            {
                return NotFound();
            }

            // MAJOR FIX: Explicitly pointing to the plural path.
            return View("~/Views/Disasters/Details.cshtml", disaster);
        }

        // GET: /Disaster/Create
        // This is the "Report a Disaster" page
        [Authorize]
        public IActionResult Create()
        {
            // MAJOR FIX: Explicitly pointing to the plural path.
            return View("~/Views/Disaster/Create.cshtml");
        }

        // POST: /Disaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DisasterName,Location,StartDate,Description")] Disaster disaster)
        {
            disaster.EndDate = null;

            if (ModelState.IsValid)
            {
                _context.Add(disaster);
                await _context.SaveChangesAsync();

                // Redirect to the "Index" action of this *same* controller
                return RedirectToAction(nameof(Index));
            }

            // If the form is invalid, show it again using the explicit path
            return View("~/Views/Disasters/Create.cshtml", disaster);
        }
    }
}

