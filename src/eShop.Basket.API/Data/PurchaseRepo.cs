public class PurchaseRepo : IPurchaseRepo
{
    private readonly AppDbContext _context;

    public PurchaseRepo(AppDbContext context)
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

    public void CreatePurchase(int ProductId, Purchase purchase)
    {
        if (purchase == null)
            {
                throw new ArgumentNullException(nameof(purchase));
            }

            purchase.ProductId = ProductId;
            _context.Purchases.Add(purchase);
    }

    public IEnumerable<Purchase> GetAllPurchases()
    {
        return _context.Purchases.ToList();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Purchase GetPurchase(int productId, int purchaseId)
    {
        return _context.Purchases
            .Where(x=>x.ProductId == productId && x.Id == purchaseId).FirstOrDefault();
    }

    public IEnumerable<Purchase> GetPurchaseForProduct(int ProductId)
    {
        return _context.Purchases
            .Where(x=>x.ProductId == ProductId)
            .OrderBy(x=>x.product.Name);
    }

    public bool ProductExist(int productId)
    {
        return _context.Products.Any(x=>x.Id == productId);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public Product GetProduct(int productId)
    {
        return _context.Products.FirstOrDefault(x=>x.Id == productId);
    }
}