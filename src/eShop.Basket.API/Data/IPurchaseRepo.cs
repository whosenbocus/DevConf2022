public interface IPurchaseRepo
{
    bool SaveChanges();

    void CreatePurchase(int ProductId, Purchase purchase);
    IEnumerable<Purchase> GetAllPurchases();
    Purchase GetPurchase(int productId, int purchaseId);
    IEnumerable<Purchase> GetPurchaseForProduct(int ProductId);



    void CreateProduct(Product product);
    IEnumerable<Product> GetAllProducts();
    bool ProductExist(int productId);
    Product GetProduct(int productId);
}