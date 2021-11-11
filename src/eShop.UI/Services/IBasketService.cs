public interface IBasketService
{
    Task<PurchaseRead> GetPurchasesForProduct(string productId);
    Task<PurchaseRead> GetPurchasesForProduct(string productId, string purchaseId);
    Task CreatePurchaseForProduct(string productId, PurchaseCreate purchase);

    Task<IEnumerable<BasketProductRead>> GetProducts();
}