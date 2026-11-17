
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioWPFWApi.Data;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Models;
using PortfolioWPFWApi.Security;
using Xunit;

namespace Tests;

public class ProjectsControllerTests : IAsyncLifetime
{
    private readonly ApiWebApplicationFactory _factory = new();
    private readonly string _testPrefix = $"ProjectTest_{Guid.NewGuid():N}";
    private string? _testUserId;

    [Fact]
    public async Task GetAll_AnonymousUser_ReturnsOk()
    {
        // Arrange
        var projectId = await AddTestProjectAsync();
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync("/api/Projects");

        var projects = await response.Content
            .ReadFromJsonAsync<List<ProjectDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(projects);
        Assert.Contains(projects, project => project.Id == projectId);
    }

    [Fact]
    public async Task GetById_ExistingProject_ReturnsOk()
    {
        // Arrange
        var projectId = await AddTestProjectAsync();
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync(
            $"/api/Projects/{projectId}");

        var project = await response.Content
            .ReadFromJsonAsync<ProjectDto>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(project);
        Assert.Equal(projectId, project.Id);
    }

    [Fact]
    public async Task GetById_UnknownProject_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync("/api/Projects/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_Admin_ReturnsCreated()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);
        var newProject = NewProject("created");

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/Projects", newProject);

        var createdProject = await response.Content
            .ReadFromJsonAsync<ProjectDto>();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(createdProject);
        Assert.Equal(newProject.Title, createdProject.Title);
    }

    [Fact]
    public async Task Post_Bezoeker_ReturnsForbidden()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Bezoeker);
        var newProject = NewProject("forbidden");

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/Projects", newProject);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Put_Admin_ReturnsNoContent()
    {
        // Arrange
        var projectId = await AddTestProjectAsync();
        using var client = await LoginAsAsync(AppRoles.Admin);

        var updatedProject = new UpdateProjectDto
        {
            Title = $"{_testPrefix}_updated",
            Description = "Bijgewerkte beschrijving",
            Tags = ["xunit"],
            Goal = "Bijwerken testen",
            Learned = "PUT-request"
        };

        // Act
        using var response = await client.PutAsJsonAsync(
            $"/api/Projects/{projectId}", updatedProject);

        using var getResponse = await client.GetAsync(
            $"/api/Projects/{projectId}");

        var project = await getResponse.Content
            .ReadFromJsonAsync<ProjectDto>();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.NotNull(project);
        Assert.Equal(updatedProject.Title, project.Title);
    }

    [Fact]
    public async Task Put_UnknownProject_ReturnsNotFound()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);

        var updatedProject = new UpdateProjectDto
        {
            Title = $"{_testPrefix}_missing",
            Description = "Beschrijving",
            Tags = ["test"],
            Goal = "Testdoel",
            Learned = "Testresultaat"
        };

        // Act
        using var response = await client.PutAsJsonAsync(
            "/api/Projects/-1", updatedProject);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Admin_ReturnsNoContent()
    {
        // Arrange
        var projectId = await AddTestProjectAsync();
        using var client = await LoginAsAsync(AppRoles.Admin);

        // Act
        using var response = await client.DeleteAsync(
            $"/api/Projects/{projectId}");

        using var getResponse = await client.GetAsync(
            $"/api/Projects/{projectId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_UnknownProject_ReturnsNotFound()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);

        // Act
        using var response = await client.DeleteAsync(
            "/api/Projects/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("POST", "/api/Projects")]
    [InlineData("PUT", "/api/Projects/-1")]
    [InlineData("DELETE", "/api/Projects/-1")]
    public async Task Write_AnonymousUser_ReturnsUnauthorized(
        string method,
        string url)
    {
        // Arrange
        using var client = CreateClient();

        using var request = new HttpRequestMessage(
            new HttpMethod(method), url);

        if (method != "DELETE")
        {
            request.Content = JsonContent.Create(
                NewProject("anonymous"));
        }

        // Act
        using var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private HttpClient CreateClient()
    {
        return _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = false
            });
    }

    private CreateProjectDto NewProject(string name)
    {
        return new CreateProjectDto
        {
            Title = $"{_testPrefix}_{name}",
            Description = "Beschrijving van testproject",
            Tags = ["wpfw", "xunit"],
            Goal = "HTTP-test uitvoeren",
            Learned = "Integratietesten"
        };
    }

    private async Task<int> AddTestProjectAsync()
    {
        var data = NewProject("existing");

        var project = new Project
        {
            Title = data.Title,
            Description = data.Description,
            Tags = data.Tags,
            Goal = data.Goal,
            Learned = data.Learned
        };

        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        db.Projects.Add(project);
        await db.SaveChangesAsync();

        return project.Id;
    }

    private async Task<HttpClient> LoginAsAsync(string role)
    {
        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var email = $"project-{Guid.NewGuid():N}@example.test";
        const string password = "TestPassword123!";

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var createResult = await userManager.CreateAsync(
            user, password);

        Assert.True(
            createResult.Succeeded,
            string.Join(", ",
                createResult.Errors.Select(
                    error => error.Description)));

        _testUserId = user.Id;

        var roleResult = await userManager.AddToRoleAsync(user, role);

        Assert.True(
            roleResult.Succeeded,
            string.Join(", ",
                roleResult.Errors.Select(
                    error => error.Description)));

        var client = CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginReqDto
            {
                Email = email,
                Password = password
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return client;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        try
        {
            using var scope = _factory.Services.CreateScope();

            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var testProjects = await db.Projects
                .Where(project =>
                    project.Title.StartsWith(_testPrefix))
                .ToListAsync();

            db.Projects.RemoveRange(testProjects);
            await db.SaveChangesAsync();

            if (_testUserId is not null)
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByIdAsync(
                    _testUserId);

                if (user is not null)
                {
                    var deleteResult = await userManager.DeleteAsync(
                        user);

                    Assert.True(
                        deleteResult.Succeeded,
                        string.Join(", ",
                            deleteResult.Errors.Select(
                                error => error.Description)));
                }
            }
        }
        finally
        {
            _factory.Dispose();
        }
    }
}
