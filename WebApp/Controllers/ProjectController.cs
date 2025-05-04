using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController(ProjectService projectService, UserService userService) : Controller
{
    private readonly ProjectService _projectService = projectService;
    private readonly UserService _userService = userService;

    // GET

    public async Task<IActionResult> Dashboard()
    {
        var user = await _userService.GetCurrentUserAsync(User);
        if (user == null)
            return Unauthorized(); // Or redirect to login

        var viewModel = new TeamMemberDisplay(user);
        ViewData["CurrentUser"] = viewModel;

        var projectCards = await _projectService.GetAllProjectsAsync();
        return View(projectCards);
    }
    public IActionResult Add()
    {
        return View(new ProjectFormModel
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(30)
        });
    }
    public async Task<IActionResult> Edit(Guid id)
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
    public async Task<IActionResult> MarkAsCompleted(Guid projectId)
    {
        await _projectService.MarkProjectAsCompletedAsync(projectId);
        return RedirectToAction("Dashboard");
    }
}
 