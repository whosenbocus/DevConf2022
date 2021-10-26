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
                new Product() {Name = "Laptop",ExternalID = 1},
                new Product() {Name = "Mouse",ExternalID = 2},
                new Product() {Name = "Keyboard",ExternalID  =3}
            );
            context.Purchases.AddRange(
                new Purchase(){ProductId = 2,Quantity = 2,Buyer = "Tom"},
                new Purchase(){ProductId = 2,Quantity = 4,Buyer = "Jane"},
                new Purchase(){ProductId = 1,Quantity = 2,Buyer = "Michael"},
                new Purchase(){ProductId = 2,Quantity = 1,Buyer = "Kevin"},
                new Purchase(){ProductId = 3,Quantity = 1,Buyer = "Jenifer"}
            );
            context.SaveChanges();
        }
    }
}