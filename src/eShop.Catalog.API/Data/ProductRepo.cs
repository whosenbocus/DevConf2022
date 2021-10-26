public class ProductRepo : IProductRepo
{
    private readonly AppDbContext _context;

    public ProductRepo(AppDbContext context)
    {
        _context = context;
    }
    public void CreateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Products.Add(product);
    }

    public void DecreaseProductQuantity(int id, int Amount)
    {
        var product = GetProductById(id);
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        product.Quantity = product.Quantity - Amount;
        _context.Products.Update(product);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}