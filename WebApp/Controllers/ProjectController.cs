using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProjectController : Controller
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }
    // GET: ProjectController
    public async Task<IActionResult> Dashboard()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return View(projects);
    }

    [HttpPost]
    public async Task<IActionResult> MarkAsCompleted(Guid projectId)
    {
        await _projectService.MarkProjectAsCompletedAsync(projectId);
        return RedirectToAction("Dashboard");
    }
}
