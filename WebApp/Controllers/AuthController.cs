using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Domain;
using WebApp.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AuthController(UserService userService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.CreateAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Dashboard", "Project");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // re-render the page with client-side errors

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction("Dashboard", "Project");

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

