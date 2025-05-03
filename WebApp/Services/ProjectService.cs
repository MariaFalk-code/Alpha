using System;
using WebApp.Models.Domain;
using WebApp.Models.ViewModels;

namespace WebApp.Services;

public class ProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddProjectAsync(ProjectFormModel model)
    {
        var project = new ProjectEntity
        {
            ProjectName = model.ProjectName,
            ClientName = model.ClientName,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Budget = model.Budget
        };
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task EditProjectAsync(Guid id, EditProjectFormModel model)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            project.ProjectName = model.ProjectName;
            project.ClientName = model.ClientName;
            project.Description = model.Description;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;
            project.Budget = model.Budget;

            await _context.SaveChangesAsync();
        }
    }
    public async Task GetProjectById(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        return project;
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
