using System.Text;
using System.Text.Json;
using Dapr.Client;
using RabbitMQ.Client;

public class MessageBusClient : IMessageBusClient
{
    //private readonly IConfiguration _configuration;
    //private readonly IConnection _connection;
    //private readonly IModel _channel;

    //public MessageBusClient(IConfiguration configuration)
    //{
    //     _configuration = configuration;
    //     var factory = new ConnectionFactory()
    //     {
    //         HostName = _configuration.GetServiceUri("rabbit","mq_binding").Host,
    //         Port = _configuration.GetServiceUri("rabbit","mq_binding").Port
    //     };


    //     try
    //     {
    //         _connection = factory.CreateConnection();
    //         _channel = _connection.CreateModel();

    //         _channel.QueueDeclare("Purchase",
    //                                 durable: false,
    //                                 exclusive:false,
    //                                 autoDelete:false,
    //                                 arguments:null);

    //         _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

    //         _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;


    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
    //     }

    // }

    // private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    // {
    // }


    // private void SendMessage(string message)
    //     {
    //         var body = Encoding.UTF8.GetBytes(message);

    //         _channel.BasicPublish(exchange: "",
    //                         routingKey: "Purchase",
    //                         basicProperties: null,
    //                         body: body);
    //     }

    //     public void Dispose()
    //     {
    //         if (_channel.IsOpen)
    //         {
    //             _channel.Close();
    //             _connection.Close();
    //         }
    //     }
    // public void PublishPurchase(int productId, int amount)
    // {
    //     var purchase = new {productId = productId, amount = amount};
    //     var message = JsonSerializer.Serialize(purchase);

    //     if (_connection.IsOpen)
    //     {
    //         SendMessage(message);
    //     }
    // }

    public async Task PublishPurchase(int productId, int amount)
    {
        var purchase = new {productId = productId, amount = amount};

        var daprClient = new DaprClientBuilder().Build();
        await daprClient.PublishEventAsync<Object>("eshopqueue","Purchase",purchase);
    }
}
