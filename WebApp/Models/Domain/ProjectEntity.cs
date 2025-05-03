using WebApp.Helpers;

namespace WebApp.Models.Domain
{
    public enum ProjectStatus
    {
        NotStarted,
        Started,
        Completed
    }
    public class ProjectEntity
    {
        public Guid ProjectId { get; set; } = GuidHelper.GenerateGuid();
        public string ProjectName { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public decimal Budget { get; set; }
        public List<TeamMemberEntity> TeamMembers { get; set; } = new();

    }
}
