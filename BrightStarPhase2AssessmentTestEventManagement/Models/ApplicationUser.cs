using Microsoft.AspNetCore.Identity;

namespace BrightStarPhase2AssessmentTestEventManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
