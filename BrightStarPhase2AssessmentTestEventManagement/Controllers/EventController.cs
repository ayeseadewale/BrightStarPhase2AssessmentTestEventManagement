using BrightStarPhase2AssessmentTestEventManagement.Data;
using BrightStarPhase2AssessmentTestEventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightStarPhase2AssessmentTestEventManagement.Controllers
{
    [Authorize(Roles = "Admin, Organizer")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Date,Time,Location,MaxParticipants")] Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
                }
            }

            // Log any validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(@event);
        }


        // GET: Event/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Location,Description")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // Utility method to check if an event exists
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
