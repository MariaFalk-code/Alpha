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
        public Guid Id { get; set; } = GuidGenerator.NewGuid();
        public string ProjectName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public List<TeamMemberEntity> TeamMembers { get; set; } = new();

    }
}
