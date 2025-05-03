using WebApp.Helpers;
using WebApp.Models.Domain;

namespace WebApp.Models.ViewModels;

public class TeamMemberDisplay
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string ProfileImageUrl => AvatarHelper.GenerateAvatarUrl(FullName);
}
