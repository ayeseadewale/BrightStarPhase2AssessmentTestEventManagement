using BrightStarPhase2AssessmentTestEventManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrightStarPhase2AssessmentTestEventManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var events = await _context.Events
                .Include(e => e.Participants)
                .Where(e => e.Participants.Any(p => p.UserId == userId))
                .ToListAsync();

            return View(events);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var events = await _context.Events.Include(e => e.Participants).ToListAsync();
            return View(events);
        }
    }

}
