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
    public async Task<IActionResult> Register(RegisterFormModel model)
    {
        if (!ModelState.IsValid)
            return PartialView("_RegisterModal", model); // Show form again with validation errors

        var result = await _userService.CreateAsync(model);

        if (result == 200)
            return RedirectToAction("Dashboard", "Project");

        if (result == 409)
        {
            ModelState.AddModelError("Email", "An account with this email already exists.");
                return PartialView("_RegisterModal", model);
            }

        ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            return PartialView("_RegisterModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_LoginModal", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return PartialView("_LoginModal", model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Dashboard", "Project");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return PartialView("_LoginModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }

