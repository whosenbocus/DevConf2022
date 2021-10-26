using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasMany(p=>p.Purchases)
            .WithOne(p=>p.product!)
            .HasForeignKey(p=>p.ProductId);
        
        modelBuilder
                .Entity<Purchase>()
                .HasOne(p => p.product)
                .WithMany(p => p.Purchases)
                .HasForeignKey(p =>p.ProductId);
    }
}