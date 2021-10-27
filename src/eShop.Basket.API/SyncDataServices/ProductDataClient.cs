public class ProductDataClient : IProductDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProductDataClient(
        HttpClient httpClient,
        IConfiguration configuration
        )
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task DecreaseProduct(int productId, int amount)
    {
        var response = await _httpClient.PutAsync($"{_configuration["CatalogService"]}/api/Catalog/Product/{productId}?Amount={amount}",null);
    }
}