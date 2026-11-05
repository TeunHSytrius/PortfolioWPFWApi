namespace PortfolioWPFWApi.DTOs;

public class CreateProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string Goal { get; set; } = string.Empty;
    public string Learned { get; set; } = string.Empty;
}