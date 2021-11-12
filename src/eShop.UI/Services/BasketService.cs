using Microsoft.AspNetCore.Components;

public class BasketService : IBasketService
{
    private readonly HttpClient _http;

    public BasketService(HttpClient http)
    {
        _http = http;
    }
    public async Task<PurchaseRead> CreatePurchaseForProduct(string productId, PurchaseCreate purchase)
    {
        return await _http.PostJsonAsync<PurchaseRead>($"api/Basket/Purchase?productId={productId}",purchase);
    }

    public async Task<IEnumerable<BasketProductRead>> GetProducts()
    {
        return await _http.GetJsonAsync<IEnumerable<BasketProductRead>>("api/Basket/Product");
    }

    public async Task<IEnumerable<PurchaseRead>> GetPurchasesForProduct(string productId)
    {
        return await _http.GetJsonAsync<IEnumerable<PurchaseRead>>($"api/Basket/Purchase?productId={productId}");
    }

    public async Task<IEnumerable<PurchaseRead>> GetPurchasesForProduct(string productId, string purchaseId)
    {
        PurchaseRead purchaseRead = await _http.GetJsonAsync<PurchaseRead>($"api/Basket/Purchase/{purchaseId}?productId={productId}");
        return new List<PurchaseRead>()
        {
            purchaseRead
        };
    }
}