using WebApp.Helpers;

namespace WebApp.Models;

public class ProjectCardViewModel
{
    public string ProjectName { get; set; }
    public string ClientName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public string Deadline => CalculateDeadline.GetDeadline(StartDate, EndDate);
    public List<TeamMemberViewModel> TeamMembers { get; set; } = new();
}
