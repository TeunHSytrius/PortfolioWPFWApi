namespace PortfolioWPFWApi.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string Goal { get; set; } = string.Empty;
    public string Learned { get; set; } = string.Empty;
}