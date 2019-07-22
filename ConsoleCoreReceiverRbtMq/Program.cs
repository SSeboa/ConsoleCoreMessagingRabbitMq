using ConnectionManager;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleCoreReceiverRbtMq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello this is the Receiver application!");

            var factory = new ConnectionFactoryCreator().Get();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;                   
                    string[] message = Encoding.UTF8.GetString(body).Split(',');
                    Console.WriteLine($"Hello {message[1]}, I am your father! ");
                };

                channel.BasicConsume(queue: "msgKey",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
