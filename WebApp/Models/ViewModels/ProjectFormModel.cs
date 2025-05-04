using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ProjectFormModel
    {
        [Required(ErrorMessage = "Project name is required.")]
        public string ProjectName { get; set; } = null!;

        [Required(ErrorMessage = "Client name is required.")]
        public string ClientName { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "An estimated budget is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Budget must be greater than 0.")]
        public decimal Budget { get; set; }

        public List<Guid> SelectedTeamMemberIds { get; set; } = new();
    }

    public class EditProjectFormModel : ProjectFormModel
    {
        public Guid ProjectId { get; set; }
    }
}
