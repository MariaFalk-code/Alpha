using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController : Controller
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }
    // GET
    public async Task<IActionResult> Dashboard()
    {
        var projectCards = await _projectService.GetAllProjectsAsync();
        return View(projectCards);
    }
    public IActionResult Add()
    {
        return View(new ProjectFormModel());
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        var model = new EditProjectFormModel
        {
            ProjectId = project.Id,
            ProjectName = project.Name,
            ClientName = project.Client.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget
        };

        return PartialView("_EditProjectModal", model);

    }

    //POST
    [HttpPost]
    public IActionResult Add(ProjectFormModel model)
    {
        if (!ModelState.IsValid)
            return ViewComponent("_AddProjectModal", model);
        
        //To be continued

        return RedirectToAction("Dashboard");
    }
    [HttpPost]
    public IActionResult Edit(EditProjectFormModel model)
    {
        if (!ModelState.IsValid)
            return ViewComponent("_EditProjectModal", model);

        //To be continued

        return RedirectToAction("Dashboard");
    }
    [HttpPost]
    public async Task<IActionResult> MarkAsCompleted(Guid projectId)
    {
        await _projectService.MarkProjectAsCompletedAsync(projectId);
        return RedirectToAction("Dashboard");
    }
}
 