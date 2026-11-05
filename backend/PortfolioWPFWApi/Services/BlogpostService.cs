
using Microsoft.EntityFrameworkCore;
using PortfolioWPFWApi.Data;
using PortfolioWPFWApi.DTOs;
using PortfolioWPFWApi.Models;

namespace PortfolioWPFWApi.Services;

public class BlogpostService
{
    private readonly ApplicationDbContext _dbContext;
    public BlogpostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<BlogpostDto>> GetAllBlogpostsAsync()
    {
        var blogposts = await _dbContext.Blogposts
            .AsNoTracking()
            .ToListAsync();
        return blogposts.Select(MapToBlogpostDto).ToList();
    }
    public async Task<BlogpostDto?> GetBlogpostByIdAsync(int blogpostId)
    {
        var blogpost = await _dbContext.Blogposts
            .AsNoTracking()
            .FirstOrDefaultAsync(blogpost => blogpost.Id == blogpostId);
        return blogpost == null ? null : MapToBlogpostDto(blogpost);
    }
    public async Task<BlogpostDto> CreateBlogpostAsync(
        CreateBlogpostDto createBlogpostDto)
    {
        var newBlogpost = new Blogpost
        {
            Title = createBlogpostDto.Title,
            Content = createBlogpostDto.Content,
            PublicationDate = createBlogpostDto.PublicationDate
        };
        _dbContext.Blogposts.Add(newBlogpost);
        await _dbContext.SaveChangesAsync();
        return MapToBlogpostDto(newBlogpost);
    }
    public async Task<bool> UpdateBlogpostAsync(
        int blogpostId,
        UpdateBlogpostDto updateBlogpostDto)
    {
        var existingBlogpost = await _dbContext.Blogposts.FindAsync(blogpostId);

        if (existingBlogpost == null)
        {
            return false;
        }
        existingBlogpost.Title = updateBlogpostDto.Title;
        existingBlogpost.Content = updateBlogpostDto.Content;
        existingBlogpost.PublicationDate = updateBlogpostDto.PublicationDate;
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteBlogpostAsync(int blogpostId)
    {
        var existingBlogpost = await _dbContext.Blogposts.FindAsync(blogpostId);
        if (existingBlogpost == null)
        {
            return false;
        }
        _dbContext.Blogposts.Remove(existingBlogpost);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    private static BlogpostDto MapToBlogpostDto(Blogpost blogpost)
    {
        return new BlogpostDto
        {
            Id = blogpost.Id,
            Title = blogpost.Title,
            Content = blogpost.Content,
            PublicationDate = blogpost.PublicationDate
        };
    }
}
