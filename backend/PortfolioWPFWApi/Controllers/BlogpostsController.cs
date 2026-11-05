
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Security;
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
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<BlogpostDto>>> GetAllBlogpostsAsync()
    {
        var blogposts = await _blogpostService.GetAllBlogpostsAsync();
        return Ok(blogposts);
    }
    [HttpGet("{blogpostId:int}", Name = "GetBlogpostById")]
    [AllowAnonymous]
    public async Task<ActionResult<BlogpostDto>> GetBlogpostByIdAsync(int blogpostId)
    {
        var blogpost = await _blogpostService.GetBlogpostByIdAsync(blogpostId);
        if (blogpost == null)
        {
            return NotFound();
        }
        return Ok(blogpost);
    }
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<ActionResult<BlogpostDto>> CreateBlogpostAsync(
        CreateBlogpostDto createBlogpostDto)
    {
        var createdBlogpost = await _blogpostService.CreateBlogpostAsync(createBlogpostDto);
        return CreatedAtRoute(
            "GetBlogpostById",
            new { blogpostId = createdBlogpost.Id },
            createdBlogpost);
    }
    [HttpPut("{blogpostId:int}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<IActionResult> UpdateBlogpostAsync(
        int blogpostId,
        UpdateBlogpostDto updateBlogpostDto)
    {
        var blogpostUpdated = await _blogpostService.UpdateBlogpostAsync(
            blogpostId, updateBlogpostDto);
        if (!blogpostUpdated)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpDelete("{blogpostId:int}")]
    [Authorize(Policy = AuthorizationPolicies.CanManageContent)]
    public async Task<IActionResult> DeleteBlogpostAsync(int blogpostId)
    {
        var blogpostDeleted = await _blogpostService.DeleteBlogpostAsync(blogpostId);
        if (!blogpostDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
