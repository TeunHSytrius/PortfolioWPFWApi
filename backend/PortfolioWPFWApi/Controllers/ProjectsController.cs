using Microsoft.AspNetCore.Mvc;
using PortfolioWPFWApi.Services;

namespace PortfolioWPFWApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectsController(ProjectService projectService)
    {
        _projectService = projectService;
    }
}