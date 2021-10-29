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
        DecreaseAmount(message);
    }

    private void DecreaseAmount(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepo>();
                
                PurchasePublishDto DecreaseAmount = JsonSerializer.Deserialize<PurchasePublishDto>(message);
                try
                {
                    repo.DecreaseProductQuantity(DecreaseAmount.productId, DecreaseAmount.amount);
                    repo.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
    }
}