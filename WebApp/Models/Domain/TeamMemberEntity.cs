using WebApp.Helpers;

namespace WebApp.Models.Domain;

public class TeamMemberEntity
{
    public Guid Id { get; set; } = GuidHelper.GenerateGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

}
