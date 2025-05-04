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
    }
}
