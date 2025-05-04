using WebApp.Helpers;
using WebApp.Models.Domain;

namespace WebApp.Models.ViewModels;

public class TeamMemberDisplay
{
    public string FullName { get; }
    public string ProfileImageUrl { get; }

    public TeamMemberDisplay(AppUser user)
    {
        FullName = $"{user.FirstName} {user.LastName}";
        ProfileImageUrl = AvatarHelper.GenerateAvatarUrl(FullName);
    }
}
