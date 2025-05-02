using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ProjectController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
