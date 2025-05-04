using Microsoft.AspNetCore.Identity;

namespace WebApp.Models.Domain
{
    public class AppUser : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;
        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    }
}
