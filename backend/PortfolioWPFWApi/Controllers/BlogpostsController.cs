using Microsoft.AspNetCore.Mvc;
using PortfolioWPFWApi.Services;

namespace PortfolioWPFWApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogpostsController : ControllerBase
{
    private readonly BlogpostService _blogpostService;

    public BlogpostsController(BlogpostService blogpostService)
    {
        _blogpostService = blogpostService;
    }
}