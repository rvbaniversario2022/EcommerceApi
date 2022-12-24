using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using WebApplication2.Data;

namespace IntegrationTest
{
    public class WebApiApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDataContext>));
                services.AddDbContextFactory<AppDataContext>(o =>
                o.UseSqlite("DataSource=file::memory:"));

                services.BuildServiceProvider().GetService<AppDataContext>()!.Database.EnsureCreated();
                //services.AddDbContext<AppDataContext>(options =>
                //    options.UseInMemoryDatabase("Testing", root));
            });

            return base.CreateHost(builder);
        }
    }
}
