using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiftGivers.Data;
using GiftGivers.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiftGivers.Controllers
{
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

        // GET: Disasters/Details/5
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

            // This will now correctly find /Views/Disasters/Details.cshtml
            return View(disaster);
        }

        // GET: Disasters/Create
        [Authorize]
        public IActionResult Create()
        {
            // This will now correctly find /Views/Disasters/Create.cshtml
            return View();
        }

        // POST: Disasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DisasterName,Location,StartDate,Description")] Disaster disaster)
        {
            if (ModelState.IsValid)
            {
                disaster.EndDate = null;

                _context.Add(disaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disaster);
        }
    }
}

