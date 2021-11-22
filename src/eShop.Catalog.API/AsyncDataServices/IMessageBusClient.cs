public interface IMessageBusClient
{
    Task PublishNewProduct(int id, string name);
}