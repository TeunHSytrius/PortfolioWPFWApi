using PortfolioWPFWApi.Data;

namespace PortfolioWPFWApi.Services;

public class ProjectService {
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }
}