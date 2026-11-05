
using Microsoft.EntityFrameworkCore;
using PortfolioWPFWApi.Data;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Models;

namespace PortfolioWPFWApi.Services;

public class ProjectService
{
    private readonly ApplicationDbContext _dbContext;
    public ProjectService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _dbContext.Projects
            .AsNoTracking()
            .ToListAsync();
        return projects.Select(MapToProjectDto).ToList();
    }
    public async Task<ProjectDto?> GetProjectByIdAsync(int projectId)
    {
        var project = await _dbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(project => project.Id == projectId);
        return project == null ? null : MapToProjectDto(project);
    }
    public async Task<ProjectDto> CreateProjectAsync(
        CreateProjectDto createProjectDto)
    {
        var newProject = new Project
        {
            Title = createProjectDto.Title,
            Description = createProjectDto.Description,
            Tags = createProjectDto.Tags.ToList(),
            Goal = createProjectDto.Goal,
            Learned = createProjectDto.Learned
        };
        _dbContext.Projects.Add(newProject);
        await _dbContext.SaveChangesAsync();
        return MapToProjectDto(newProject);
    }
    public async Task<bool> UpdateProjectAsync(
        int projectId,
        UpdateProjectDto updateProjectDto)
    {
        var existingProject = await _dbContext.Projects.FindAsync(projectId);

        if (existingProject == null)
        {
            return false;
        }
        existingProject.Title = updateProjectDto.Title;
        existingProject.Description = updateProjectDto.Description;
        existingProject.Tags = updateProjectDto.Tags.ToList();
        existingProject.Goal = updateProjectDto.Goal;
        existingProject.Learned = updateProjectDto.Learned;
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        var existingProject = await _dbContext.Projects.FindAsync(projectId);
        if (existingProject == null)
        {
            return false;
        }
        _dbContext.Projects.Remove(existingProject);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    private static ProjectDto MapToProjectDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Tags = project.Tags.ToList(),
            Goal = project.Goal,
            Learned = project.Learned
        };
    }
}
