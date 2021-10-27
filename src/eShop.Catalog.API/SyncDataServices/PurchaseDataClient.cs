using System.Text;
using System.Text.Json;

public class PurchaseDataClient : IPurchaseDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PurchaseDataClient(
        HttpClient httpClient,
        IConfiguration configuration
        )
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task CreateProduct(int id, string name)
    {
        var product = new {externalID = id, name = name};
        var httpContent = new StringContent(
                JsonSerializer.Serialize(product),
                Encoding.UTF8,
                "application/json");
                
        await _httpClient.PostAsync($"{_configuration["BasketService"]}/api/Basket/Product/",httpContent);
    }
}