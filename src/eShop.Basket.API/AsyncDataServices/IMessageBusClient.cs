public interface IMessageBusClient
{
    Task PublishPurchase(int productId, int amount);
}