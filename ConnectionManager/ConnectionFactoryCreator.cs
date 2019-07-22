using System.Configuration;
using RabbitMQ.Client;

namespace ConnectionManager
{
    //Factory pattern to create an instance of a connection factory
    public class ConnectionFactoryCreator : IConnectionFactory
    {
        /// <summary>
        /// This method is responsible to get the connection to RabbitMQ server
        /// </summary>
        /// <returns></returns>
        public ConnectionFactory Get()
        {
            return new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["hostname"] ?? "localhost"
            };
        }
      
    }
}
