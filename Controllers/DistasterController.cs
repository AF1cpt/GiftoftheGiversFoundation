using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GiftGivers.Controllers
{
    [Authorize]
    public class DisastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Disasters.ToListAsync());
        }

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisasterId,DisasterName,Location,StartDate,Description")] Disaster disaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disaster);
        }
    }
}