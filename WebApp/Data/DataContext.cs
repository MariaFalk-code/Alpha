using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Domain;

namespace WebApp.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<ClientEntity> Clients { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AppUser>().ToTable("Users");
        builder.Entity<ProjectEntity>().ToTable("Projects");
        builder.Entity<ClientEntity>().ToTable("Clients");

        builder.Entity<ProjectEntity>()
        .HasMany(p => p.TeamMembers)
        .WithMany(u => u.Projects)
        .UsingEntity(j => j.ToTable("ProjectTeamMembers"));

        builder.Entity<ProjectEntity>()
        .Property(p => p.Status)
        .HasConversion<string>();

        builder.Entity<ProjectEntity>().HasData(
            new ProjectEntity
            {
                ProjectId = 1,
                ProjectName = "Website Redesign",
                ClientName = "GitLab Inc.",
                Description = "Redesign the corporate website to improve UX and visual appeal.",
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 7, 1),
                Budget = 20000,
                Status = ProjectStatus.NotStarted
            },
            new ProjectEntity
            {
                ProjectId = 2,
                ProjectName = "Landing Page",
                ClientName = "Bitbucket, Inc.",
                Description = "Build a new landing page for product launch.",
                StartDate = new DateTime(2024, 5, 15),
                EndDate = new DateTime(2024, 6, 15),
                Budget = 10000,
                Status = ProjectStatus.Started
            },
            new ProjectEntity
            {
                ProjectId = 3,
                ProjectName = "Parser Development",
                ClientName = "Parser Systems",
                Description = "Develop a ticket site parser in Python.",
                StartDate = new DateTime(2024, 4, 10),
                EndDate = new DateTime(2024, 5, 10),
                Budget = 15000,
                Status = ProjectStatus.Completed
            }
        );
    }
}
