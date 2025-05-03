namespace WebApp.Models.ViewModels
{
    public class ProjectFormModel
    {
        public string ProjectName { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public List<Guid> SelectedTeamMemberIds { get; set; } = new();
    }

    public class EditProjectFormModel : ProjectFormModel
    {
        public Guid ProjectId { get; set; }
    }
}
