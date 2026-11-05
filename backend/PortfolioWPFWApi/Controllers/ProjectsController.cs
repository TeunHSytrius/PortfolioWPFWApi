
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Security;
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
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<ProjectDto>>> GetAllProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }
    [HttpGet("{projectId:int}", Name = "GetProjectById")]
    [AllowAnonymous]
    public async Task<ActionResult<ProjectDto>> GetProjectByIdAsync(int projectId)
    {
        var project = await _projectService.GetProjectByIdAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<ActionResult<ProjectDto>> CreateProjectAsync(
        CreateProjectDto createProjectDto)
    {
        var createdProject = await _projectService.CreateProjectAsync(createProjectDto);
        return CreatedAtRoute(
            "GetProjectById",
            new { projectId = createdProject.Id },
            createdProject);
    }
    [HttpPut("{projectId:int}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<IActionResult> UpdateProjectAsync(
        int projectId,
        UpdateProjectDto updateProjectDto)
    {
        var projectUpdated = await _projectService.UpdateProjectAsync(
            projectId, updateProjectDto);
        if (!projectUpdated)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpDelete("{projectId:int}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<IActionResult> DeleteProjectAsync(int projectId)
    {
        var projectDeleted = await _projectService.DeleteProjectAsync(projectId);
        if (!projectDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
