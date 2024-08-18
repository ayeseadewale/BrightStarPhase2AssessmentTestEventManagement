using BrightStarPhase2AssessmentTestEventManagement.Data;
using BrightStarPhase2AssessmentTestEventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightStarPhase2AssessmentTestEventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }
    }

}
