using System.ComponentModel.DataAnnotations;

namespace PortfolioWPFWApi.DTOs;

public class CreateBlogpostDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }
}