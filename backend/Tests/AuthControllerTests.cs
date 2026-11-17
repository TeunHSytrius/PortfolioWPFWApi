
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Models;
using PortfolioWPFWApi.Security;
using Xunit;

namespace Tests;

public class AuthControllerTests : IAsyncLifetime
{
    private readonly ApiWebApplicationFactory _factory = new();
    private readonly string _email = $"auth-{Guid.NewGuid():N}@example.test";

    private const string Password = "TestPassword123!";

    [Fact]
    public async Task Register_ValidData_ReturnsCreated()
    {
        // Arrange
        using var client = CreateClient();

        var request = new RegisterReqDto
        {
            Email = _email,
            Password = Password
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/register", request);

        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync(_email);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(user);
        Assert.True(await userManager.IsInRoleAsync(
            user, AppRoles.Bezoeker));
    }

    [Fact]
    public async Task Register_DuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        await CreateUserAsync();
        using var client = CreateClient();

        var request = new RegisterReqDto
        {
            Email = _email,
            Password = Password
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_InvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();

        var request = new RegisterReqDto
        {
            Email = "ongeldig-email",
            Password = Password
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_ShortPassword_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();

        var request = new RegisterReqDto
        {
            Email = _email,
            Password = "short"
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        // Arrange
        await CreateUserAsync();
        using var client = CreateClient();

        var request = new LoginReqDto
        {
            Email = _email,
            Password = Password
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_WrongPassword_ReturnsUnauthorized()
    {
        // Arrange
        await CreateUserAsync();
        using var client = CreateClient();

        var request = new LoginReqDto
        {
            Email = _email,
            Password = "WrongPassword123!"
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_UnknownUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = CreateClient();

        var request = new LoginReqDto
        {
            Email = _email,
            Password = Password
        };

        // Act
        using var response = await client.PostAsJsonAsync(
            "/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Me_AnonymousUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        using var response = await client.GetAsync("/api/auth/me");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Me_LoggedInUser_ReturnsOk()
    {
        // Arrange
        using var client = await LoginAsync();

        // Act
        using var response = await client.GetAsync("/api/auth/me");

        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(_email, content);
    }

    [Fact]
    public async Task Logout_LoggedInUser_ReturnsNoContent()
    {
        // Arrange
        using var client = await LoginAsync();

        // Act
        using var logoutResponse = await client.PostAsync(
            "/api/auth/logout", null);

        using var meResponse = await client.GetAsync(
            "/api/auth/me");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, meResponse.StatusCode);
    }

    private HttpClient CreateClient()
    {
        return _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = false,
                HandleCookies = true
            });
    }

    private async Task CreateUserAsync()
    {
        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser
        {
            UserName = _email,
            Email = _email
        };

        var result = await userManager.CreateAsync(
            user, Password);

        Assert.True(
            result.Succeeded,
            string.Join(", ",
                result.Errors.Select(error => error.Description)));
    }

    private async Task<HttpClient> LoginAsync()
    {
        await CreateUserAsync();

        var client = CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginReqDto
            {
                Email = _email,
                Password = Password
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

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync(_email);

            if (user is not null)
            {
                var result = await userManager.DeleteAsync(user);

                Assert.True(
                    result.Succeeded,
                    string.Join(", ",
                        result.Errors.Select(error => error.Description)));
            }
        }
        finally
        {
            _factory.Dispose();
        }
    }
}
