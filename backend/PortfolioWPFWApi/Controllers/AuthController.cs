
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Models;
using PortfolioWPFWApi.Security;

namespace PortfolioWPFWApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterReqDto request)
    {
        var email = request.Email.Trim();

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                Errors = result.Errors.Select(
                    error => error.Description)
            });
        }

        var roleResult = await _userManager.AddToRoleAsync(
            user,
            AppRoles.Bezoeker);

        if (!roleResult.Succeeded)
        {
            var deleteResult = await _userManager.DeleteAsync(user);

            if (!deleteResult.Succeeded)
            {
                return Problem(
                    "Registratie mislukt. Het account moet worden gecontroleerd.");
            }

            return Problem("Registratie kon niet worden afgerond.");
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new
            {
                Message = "Account aangemaakt.",
                UserId = user.Id,
                Email = user.Email
            });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginReqDto request)
    {
        var user = await _userManager.FindByEmailAsync(
            request.Email.Trim());

        if (user is null)
        {
            return Unauthorized(new
            {
                Message = "Ongeldige inloggegevens."
            });
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            return Unauthorized(new
            {
                Message = "Aanmelden niet mogelijk."
            });
        }

        return Ok(new
        {
            Message = "Ingelogd.",
            UserId = user.Id,
            Email = user.Email
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(new
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }
}
