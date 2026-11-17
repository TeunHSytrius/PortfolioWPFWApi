
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

public class BlogpostsControllerTests : IAsyncLifetime
{
    private readonly ApiWebApplicationFactory _factory = new();
    private readonly string _testPrefix = $"BlogpostTest_{Guid.NewGuid():N}";
    private string? _testUserId;

    [Fact]
    public async Task GetAll_AnonymousUser_ReturnsOk()
    {
        // Arrange
        var blogpostId = await AddTestBlogpostAsync();
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync("/api/Blogposts");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var blogposts = await response.Content
            .ReadFromJsonAsync<List<BlogpostDto>>();

        Assert.NotNull(blogposts);
        Assert.Contains(blogposts, blogpost => blogpost.Id == blogpostId);
    }

    [Fact]
    public async Task GetById_ExistingBlogpost_ReturnsOk()
    {
        // Arrange
        var blogpostId = await AddTestBlogpostAsync();
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync(
            $"/api/Blogposts/{blogpostId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var blogpost = await response.Content
            .ReadFromJsonAsync<BlogpostDto>();

        Assert.NotNull(blogpost);
        Assert.Equal(blogpostId, blogpost.Id);
    }

    [Fact]
    public async Task GetById_UnknownBlogpost_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync("/api/Blogposts/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_Admin_ReturnsCreated()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);
        var newBlogpost = NewBlogpost("created");

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/Blogposts", newBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdBlogpost = await response.Content
            .ReadFromJsonAsync<BlogpostDto>();

        Assert.NotNull(createdBlogpost);
        Assert.Equal(newBlogpost.Title, createdBlogpost.Title);

        using var getResponse = await client.GetAsync(
            $"/api/Blogposts/{createdBlogpost.Id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var savedBlogpost = await getResponse.Content
            .ReadFromJsonAsync<BlogpostDto>();

        Assert.NotNull(savedBlogpost);
        Assert.Equal(createdBlogpost.Id, savedBlogpost.Id);
        Assert.Equal(newBlogpost.Title, savedBlogpost.Title);
        Assert.Equal(newBlogpost.Content, savedBlogpost.Content);
    }

    [Fact]
    public async Task Post_Bezoeker_ReturnsForbidden()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Bezoeker);
        var newBlogpost = NewBlogpost("forbidden");

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/Blogposts", newBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidPublicationDate_ReturnsBadRequest()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);

        var invalidBlogpost = new
        {
            Title = $"{_testPrefix}_invalid",
            Content = "Inhoud van testblogpost",
            PublicationDate = "geen-datum"
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/Blogposts", invalidBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_Admin_ReturnsNoContent()
    {
        // Arrange
        var blogpostId = await AddTestBlogpostAsync();
        using var client = await LoginAsAsync(AppRoles.Admin);

        var updatedBlogpost = new UpdateBlogpostDto
        {
            Title = $"{_testPrefix}_updated",
            Content = "Bijgewerkte inhoud",
            PublicationDate = DateTime.UtcNow
        };

        // Act
        using var response = await client.PutAsJsonAsync(
            $"/api/Blogposts/{blogpostId}", updatedBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var getResponse = await client.GetAsync(
            $"/api/Blogposts/{blogpostId}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var blogpost = await getResponse.Content
            .ReadFromJsonAsync<BlogpostDto>();

        Assert.NotNull(blogpost);
        Assert.Equal(updatedBlogpost.Title, blogpost.Title);
        Assert.Equal(updatedBlogpost.Content, blogpost.Content);
    }

    [Fact]
    public async Task Put_InvalidPublicationDate_ReturnsBadRequest()
    {
        // Arrange
        var blogpostId = await AddTestBlogpostAsync();
        using var client = await LoginAsAsync(AppRoles.Admin);

        var invalidBlogpost = new
        {
            Title = $"{_testPrefix}_updated",
            Content = "Bijgewerkte inhoud",
            PublicationDate = "geen-datum"
        };

        // Act
        using var response = await client.PutAsJsonAsync(
            $"/api/Blogposts/{blogpostId}", invalidBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        using var getResponse = await client.GetAsync(
            $"/api/Blogposts/{blogpostId}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var blogpost = await getResponse.Content
            .ReadFromJsonAsync<BlogpostDto>();

        Assert.NotNull(blogpost);
        Assert.Equal($"{_testPrefix}_existing", blogpost.Title);
    }

    [Fact]
    public async Task Put_UnknownBlogpost_ReturnsNotFound()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);

        var updatedBlogpost = new UpdateBlogpostDto
        {
            Title = $"{_testPrefix}_missing",
            Content = "Inhoud van testblogpost",
            PublicationDate = DateTime.UtcNow
        };

        // Act
        using var response = await client.PutAsJsonAsync(
            "/api/Blogposts/-1", updatedBlogpost);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Admin_ReturnsNoContent()
    {
        // Arrange
        var blogpostId = await AddTestBlogpostAsync();
        using var client = await LoginAsAsync(AppRoles.Admin);

        // Act
        using var response = await client.DeleteAsync(
            $"/api/Blogposts/{blogpostId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var getResponse = await client.GetAsync(
            $"/api/Blogposts/{blogpostId}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_UnknownBlogpost_ReturnsNotFound()
    {
        // Arrange
        using var client = await LoginAsAsync(AppRoles.Admin);

        // Act
        using var response = await client.DeleteAsync(
            "/api/Blogposts/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("POST", "/api/Blogposts")]
    [InlineData("PUT", "/api/Blogposts/-1")]
    [InlineData("DELETE", "/api/Blogposts/-1")]
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
                NewBlogpost("anonymous"));
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

    private CreateBlogpostDto NewBlogpost(string name)
    {
        return new CreateBlogpostDto
        {
            Title = $"{_testPrefix}_{name}",
            Content = "Inhoud van testblogpost",
            PublicationDate = DateTime.UtcNow
        };
    }

    private async Task<int> AddTestBlogpostAsync()
    {
        var data = NewBlogpost("existing");

        var blogpost = new Blogpost
        {
            Title = data.Title,
            Content = data.Content,
            PublicationDate = data.PublicationDate
        };

        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        db.Blogposts.Add(blogpost);
        await db.SaveChangesAsync();

        return blogpost.Id;
    }

    private async Task<HttpClient> LoginAsAsync(string role)
    {
        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var email = $"blogpost-{Guid.NewGuid():N}@example.test";
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

            var testBlogposts = await db.Blogposts
                .Where(blogpost =>
                    blogpost.Title.StartsWith(_testPrefix))
                .ToListAsync();

            db.Blogposts.RemoveRange(testBlogposts);
            await db.SaveChangesAsync();

            if (_testUserId is not null)
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByIdAsync(
                    _testUserId);

                if (user is not null)
                {
                    var deleteResult = await userManager.DeleteAsync(user);

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
