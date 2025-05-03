using WebApp.Helpers;

namespace WebApp.Models.ViewModels;

public class ProjectCard
{
    public string ProjectName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public string Deadline => CalculateDeadline.GetDeadline(StartDate, EndDate);
    public List<TeamMemberDisplay> TeamMembers { get; set; } = new();
}
