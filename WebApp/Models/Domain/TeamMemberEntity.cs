
namespace WebApp.Models.Domain;

// This class represents a team member in the project management system.
// It is currently not used in the application but is defined for future use.
//Should extend AppUser.
public class TeamMemberEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
