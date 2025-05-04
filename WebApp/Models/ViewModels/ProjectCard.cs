using WebApp.Helpers;

namespace WebApp.Models.ViewModels;

public class ProjectCard
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal Budget { get; set; }
    public string Deadline => CalculateDeadline.GetDeadline(EndDate);
    public List<TeamMemberDisplay> TeamMembers { get; set; } = new();
}
