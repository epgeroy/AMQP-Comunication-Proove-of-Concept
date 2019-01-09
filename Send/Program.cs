using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", VirtualHost = "/" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string[] messages = { "AMQP rocks!", "Microservices here we go!", "F**k yeah!" };

            Random random = new Random();

            do
            {
                string message = messages[random.Next(0, messages.Length)];

                var body = Encoding.UTF8.GetBytes(message);

                System.Threading.Thread.Sleep(3000);

                channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
                Console.WriteLine(" [x] Sent {0}", message);

            } while (true);

        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}