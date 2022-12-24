using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Enums;
using WebApplication2.Models;

namespace IntegrationTest
{
    public class ApiTestFixture : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace the database provider with an in-memory provider
                var serviceProvider = services.BuildServiceProvider();
                var builder = new DbContextOptionsBuilder<AppDbContext>();
                builder.UseInMemoryDatabase("InMemoryDbForTesting");
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));

                // Create a new instance of the database context and seed it with test data
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    SeedData(db);
                }
            });
        }

        private static void SeedData(AppDbContext db)
        {
            db.Orders.Add(new Order
            {
                Id = new Guid("5B06247D-3278-443B-A676-A775B0A552D9"),
                Status = Status.Pending
            });
            db.CartItems.Add(new CartItem
            {
                Id = new Guid("7A70FE90-EE42-4898-B288-60FD419ABEFB"),
                OrderId = new Guid("5B06247D-3278-443B-A676-A775B0A552D9"),
                ProductName = "Air Force 1",
                UserId = new Guid("18DFE984-80F0-4FA2-8767-EE41892D9564")
            });
            db.Users.Add(new User
            {
                Id = new Guid("18DFE984-80F0-4FA2-8767-EE41892D9564"),
                Name = "John"
            });
            db.SaveChanges();
        }
    }
}
