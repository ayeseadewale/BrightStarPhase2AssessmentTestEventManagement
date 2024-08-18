using BrightStarPhase2AssessmentTestEventManagement.Data;
using BrightStarPhase2AssessmentTestEventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrightStarPhase2AssessmentTestEventManagement.Controllers
{
    [Authorize]
    public class EventRegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventRegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Register(int eventId)
        {
            var @event = await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null || @event.Participants.Count >= @event.MaxParticipants)
            {
                return BadRequest("Event is full or does not exist.");
            }

            var participant = new Participant
            {
                EventId = eventId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Event", new { id = eventId });
        }
    }

}
