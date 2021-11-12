public interface IBasketService
{
    Task<IEnumerable<PurchaseRead>> GetPurchasesForProduct(string productId);
    Task<IEnumerable<PurchaseRead>> GetPurchasesForProduct(string productId, string purchaseId);
    Task<PurchaseRead> CreatePurchaseForProduct(string productId, PurchaseCreate purchase);

    Task<IEnumerable<BasketProductRead>> GetProducts();
}