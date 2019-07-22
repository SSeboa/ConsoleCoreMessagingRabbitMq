using RabbitMQ.Client;
using System;
using System.Text;
using LoggingService;

namespace PublisherService
{
    /// <summary>
    /// Publisher service in charge of sending a message to RabbitMQ
    /// </summary>
    public class Publish : IPublish
    {
        private ILoggerManager _logger;
        private IConnectionFactory _factory;

        public Publish(ConnectionManager.IConnectionFactory connectionFactory, ILoggerManager logger)
        {
            _factory = connectionFactory.Get();
            _logger = logger;
        }

        /// <summary>
        /// Method to send the message on the queue. Takes a string message parameter
        /// </summary>
        /// <param name="msg">The messge to be added to the queue</param>
        /// <returns></returns>
        public string SendMessage(string msg)
        {
            _logger.LogInfo($"PublisherService.Publish.SendMessage() - Start of Message DateTime: {DateTime.Now}");

            var returnMessage = $"Hello my name is, {msg}";

            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                _logger.LogInfo($"PublisherService.Publish.SendMessage() - Message {msg} - DateTime: {DateTime.Now}");

                channel.BasicPublish(exchange: "",
                                     routingKey: "msgKey",
                                     basicProperties: null,
                                     body: Encoding.UTF8.GetBytes($"Hello my name is, {msg}"));
            }

            _logger.LogInfo($"PublisherService.Publish.SendMessage() - End of Message - DateTime: {DateTime.Now}");

            return returnMessage;
        }
    }
}
