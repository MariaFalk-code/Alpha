using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.ViewModels;

namespace WebApp.Services;

//Because of timeconstraints, we are calling the database directly in the service layer, skipping the repository pattern. That is also why
//we are not using interfaces, and why we are keeping all the layers together in WebApp.
public class ProjectService
{
    private readonly DataContext _context;

    public ProjectService(DataContext context)
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

    public async Task EditProjectAsync(int id, EditProjectFormModel model)
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
    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);

    }
    public async Task<List<ProjectCard>> GetAllProjectsAsync()
    {
        var projects = await _context.Projects
            .Include(p => p.TeamMembers)
            .ToListAsync();

        foreach (var project in projects)
        {
            if (project.Status == ProjectStatus.NotStarted && DateTime.Today >= project.StartDate)
            {
                project.Status = ProjectStatus.Started;
            }
        }

        await _context.SaveChangesAsync();

        return projects.Select(project => new ProjectCard
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ClientName = project.ClientName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            Status = project.Status.ToString(),
            TeamMembers = project.TeamMembers
                .Select(user => new TeamMemberDisplay(user))
                .ToList()
        }).ToList();
    }


    public async Task MarkProjectAsCompletedAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            project.Status = ProjectStatus.Completed;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
