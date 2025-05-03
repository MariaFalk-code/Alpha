using WebApp.Helpers;
using WebApp.Models.Domain;

namespace WebApp.Models.ViewModels;

public class TeamMemberDisplay
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string ProfileImageUrl => AvatarHelper.GenerateAvatarUrl(FullName);
}
