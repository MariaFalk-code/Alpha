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
        var projects = await _projectService.GetAllProjectsAsync();
        return View(projects);
    }
    public IActionResult Add()
    {
        return View(new ProjectFormModel());
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
 