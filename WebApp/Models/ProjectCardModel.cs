namespace WebApp.Models
{
    public class ProjectCardModel
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public List<string> TeamMembers { get; set; } = new();
    }
}
