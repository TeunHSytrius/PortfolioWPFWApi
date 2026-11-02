using Microsoft.EntityFrameworkCore;
using PortfolioWPFWApi.Models;

namespace PortfolioWPFWApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options) {
        
        
    }
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Blogpost> Blogposts => Set<Blogpost>();
}