public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(context: serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product() {Name = "Laptop",Supplier="Lenovo",Quantity = 10},
                new Product() {Name = "Mouse",Supplier="Logitect",Quantity = 100},
                new Product() {Name = "Keyboard",Supplier="Dell",Quantity = 5}
            );
            context.SaveChanges();
        }
    }
}