using System.Text.Json;
using AutoMapper;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        AddProduct(message);
    }

    private void AddProduct(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPurchaseRepo>();
                
                var productDto = JsonSerializer.Deserialize<ProductCreateDto>(message);

                try
                {
                    var product = _mapper.Map<Product>(productDto);
                    repo.CreateProduct(product);
                    repo.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
    }
}