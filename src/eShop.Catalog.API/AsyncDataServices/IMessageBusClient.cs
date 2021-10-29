public interface IMessageBusClient
{
    void PublishNewProduct(int id, string name);
}