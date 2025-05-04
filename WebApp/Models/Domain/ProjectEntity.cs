using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Domain
{
    public enum ProjectStatus
    {
        NotStarted = 0,
        Started = 1,
        Completed = 2
    }
    public class ProjectEntity
    {
        [Key]
        public int ProjectId { get; set; } // Auto-incremented by default

        [Required]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
        public string ProjectName { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "Client name cannot be longer than 100 characters.")]
        public string ClientName { get; set; } = null!;

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;

        public ICollection<AppUser> TeamMembers { get; set; } = new List<AppUser>();
    }
}
