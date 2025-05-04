using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController(ProjectService projectService, UserService userService) : Controller
{
    private readonly ProjectService _projectService = projectService;
    private readonly UserService _userService = userService;

    // GET
    //This method is used to display the dashboard with all projects. it has been rewritten several times with the help of ChatGPT4o to include everything needed.
    public async Task<IActionResult> Dashboard(string? status)
    {
        var user = await _userService.GetCurrentUserAsync(User);
        if (user == null)
            return Unauthorized();

        ViewData["CurrentUser"] = new TeamMemberDisplay(user);

        var allProjects = await _projectService.GetAllProjectsAsync();

        var filteredProjects = status switch
        {
            "Started" => allProjects.Where(p => p.Status == "Started").ToList(),
            "Completed" => allProjects.Where(p => p.Status == "Completed").ToList(),
            _ => allProjects
        };

        ViewBag.Filter = status;
        ViewBag.TotalCount = allProjects.Count;
        ViewBag.StartedCount = allProjects.Count(p => p.Status == "Started");
        ViewBag.CompletedCount = allProjects.Count(p => p.Status == "Completed");

        return View(filteredProjects);
    }

    public IActionResult Add()
    {
        return View(new ProjectFormModel
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(30)
        });
    }
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound();

        var model = new EditProjectFormModel

        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ClientName = project.ClientName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget
        };

        return PartialView("_EditProjectModal", model);
    }

    //POST
    [HttpPost]
    public async Task<IActionResult> Add(ProjectFormModel model)
    {
        if (!ModelState.IsValid)
            return PartialView("_AddProjectModal", model);
        
        await _projectService.AddProjectAsync(model);

        return RedirectToAction("Dashboard");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditProjectFormModel model)
    {
        if (!ModelState.IsValid)
            return PartialView("_EditProjectModal", model);

        await _projectService.EditProjectAsync(model.ProjectId, model);

        return RedirectToAction("Dashboard");
    }
    [HttpPost]
    public async Task<IActionResult> MarkAsCompleted(int projectId)
    {
        await _projectService.MarkProjectAsCompletedAsync(projectId);
        return RedirectToAction("Dashboard");
    }
}
 