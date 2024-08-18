using System.ComponentModel.DataAnnotations;

namespace BrightStarPhase2AssessmentTestEventManagement.Models.ViewMoles
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }


}
