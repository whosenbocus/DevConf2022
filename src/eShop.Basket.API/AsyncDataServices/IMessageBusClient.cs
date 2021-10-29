public interface IMessageBusClient
{
    void PublishPurchase(int productId, int amount);
}