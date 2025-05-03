namespace WebApp.Models.Domain;

public class TeamMemberEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
}
