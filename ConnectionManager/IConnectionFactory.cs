using RabbitMQ.Client;

namespace ConnectionManager
{
    public interface IConnectionFactory
    {       
        ConnectionFactory Get();
    }
}
