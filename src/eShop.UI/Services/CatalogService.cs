using Microsoft.AspNetCore.Components;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _http;

    public CatalogService(HttpClient http)
    {
        _http = http;
    }

    public async Task CreateProduct(ProductCreate productCreate)
    {
        await _http.PostJsonAsync("api/Catalog/Product",productCreate);
    }

    public async Task<ProductRead> GetProductByID(string id)
    {
        return await _http.GetJsonAsync<ProductRead>($"api/Catalog/Product/{id}");
    }

    public async Task<IEnumerable<ProductRead>> GetProducts()
    {
        return await _http.GetJsonAsync<IEnumerable<ProductRead>>("api/Catalog/Product");
    }
}