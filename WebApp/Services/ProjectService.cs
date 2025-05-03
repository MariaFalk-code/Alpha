using System;
using WebApp.Models.Domain;

namespace WebApp.Services;

public class ProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectEntity>> GetAllProjectsAsync()
    {
        var projects = await _context.Projects.ToListAsync();

        foreach (var project in projects)
        {
            if (project.Status == ProjectStatus.NotStarted && DateTime.Today >= project.StartDate)
            {
                project.Status = ProjectStatus.Started;
            }
        }

        await _context.SaveChangesAsync();
        return projects;
    }

    public async Task MarkProjectAsCompletedAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            project.Status = ProjectStatus.Completed;
            await _context.SaveChangesAsync();
        }
    }
}
