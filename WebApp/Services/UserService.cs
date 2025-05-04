using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApp.Models.Domain;
using WebApp.Models.ViewModels;

namespace WebApp.Services;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<AppUser?> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
    {
        return await _userManager.GetUserAsync(userPrincipal);
    }

    public async Task<IdentityResult> CreateAsync(RegisterFormModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            var duplicateResult = IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateEmail",
                Description = "An account with this email already exists."
            });

            return duplicateResult;
        }

        var user = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        return result;
    }

}

