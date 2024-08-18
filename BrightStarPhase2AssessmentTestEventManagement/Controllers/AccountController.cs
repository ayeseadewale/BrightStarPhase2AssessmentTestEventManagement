using Microsoft.AspNetCore.Mvc;

namespace BrightStarPhase2AssessmentTestEventManagement.Controllers
{
    using BrightStarPhase2AssessmentTestEventManagement.Models;
    using BrightStarPhase2AssessmentTestEventManagement.Models.ViewMoles;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user with the provided information
                var newUser = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };

                // Attempt to create the user
                var creationResult = await _userManager.CreateAsync(newUser, model.Password);

                if (creationResult.Succeeded)
                {
                    // Assign the selected role to the newly created user
                    await _userManager.AddToRoleAsync(newUser, model.Role);

                    // Sign in the user
                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    // Redirect to the home page
                    return RedirectToAction("Index", "Home");
                }

                // Add errors to the model state if the creation failed
                foreach (var error in creationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we reach this point, something failed, redisplay the form with the current model
            return View(model);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
