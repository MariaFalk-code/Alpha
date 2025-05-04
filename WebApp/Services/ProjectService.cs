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
    public async Task<ProjectEntity?> GetProjectByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);

    }
    public async Task<List<ProjectCard>> GetAllProjectsAsync()
    {
        var projects = await _context.Projects.ToListAsync();
        var viewModels = new List<ProjectCard>();

        foreach (var project in projects)
        {
            if (project.Status == ProjectStatus.NotStarted && DateTime.Today >= project.StartDate)
            {
                project.Status = ProjectStatus.Started;
            }

            viewModels.Add(new ProjectCard
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                Status = project.Status,
                TeamMembers = project.TeamMembers.Select(tm => new TeamMemberDisplay
                {
                    FirstName = tm.FirstName,
                    LastName = tm.LastName
                }).ToList()
            }).ToList();
        }

        await _context.SaveChangesAsync();

        return viewModels;
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

    public async Task DeleteProjectAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
