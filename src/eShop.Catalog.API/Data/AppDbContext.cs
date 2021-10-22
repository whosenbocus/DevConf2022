using eShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}