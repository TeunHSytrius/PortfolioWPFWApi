using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortfolioWPFWApi.Data;

namespace Tests;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            var testConnection = configuration
                                     .GetConnectionString("TestConnection") ?? throw new InvalidOperationException("TestConnection ontbreekt in User Secrets.");
            var databaseName =
                new SqlConnectionStringBuilder(testConnection).InitialCatalog;
            if (databaseName != "PortfolioWPFW_Test")
            {
                throw new InvalidOperationException(
                    "De tests mogen alleen PortfolioWPFW_Test gebruiken.");
            }
            services.RemoveAll<
                IDbContextOptionsConfiguration<ApplicationDbContext>>();
            services.RemoveAll<
                DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(testConnection));
        });
    }
}