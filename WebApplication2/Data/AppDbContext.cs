using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<CartItem>? CartItems { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
