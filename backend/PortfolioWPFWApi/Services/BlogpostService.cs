using PortfolioWPFWApi.Data;

namespace PortfolioWPFWApi.Services;

public class BlogpostService
{
    private readonly ApplicationDbContext _context;

    public BlogpostService(ApplicationDbContext context)
    {
        _context = context;
    }
}