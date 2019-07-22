using Microsoft.Extensions.DependencyInjection;
using System;
using PublisherService;
using LoggingService;
using ConnectionManager;

namespace ConsoleCoreSenderRbtMq
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello this is the sender application!");

            var service = Setup();

            Console.WriteLine("Name");
            var msg = Console.ReadLine();

            var returnMsg = service.SendMessage(msg);

            Console.WriteLine(" [x] Sent {0}", returnMsg);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            DisposeServices();
        }

        private static IPublish Setup()
        {
            RegisterServices();
            return _serviceProvider.GetService<IPublish>();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IConnectionFactory, ConnectionFactoryCreator>();
            collection.AddScoped<IPublish, Publish>();
            collection.AddSingleton<ILoggerManager, LoggerManager>();
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
