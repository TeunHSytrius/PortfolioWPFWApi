using System.ComponentModel.DataAnnotations;

namespace PortfolioWPFWApi.DTOs;

public class RegisterReqDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}