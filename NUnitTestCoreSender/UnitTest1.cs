using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PublisherService;
using Moq;
using LoggingService;
using ConnectionManager;

namespace Tests
{
    public class Tests
    {
        private IServiceCollection _services;
        private ServiceProvider _provider;

        public Tests()
        {
            _services = new ServiceCollection();

            _services.AddScoped<IConnectionFactory, ConnectionFactoryCreator>();
            _services.AddScoped<IPublish, Publish>();
            _services.AddSingleton<ILoggerManager, LoggerManager>();

            _provider = _services.BuildServiceProvider();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var message = "Test";
            var serviceMock = new Mock<IPublish>();
            serviceMock.Setup(x => x.SendMessage(message)).Returns($"Hello my name is, {message}");

            var publish = new Publish(_provider.GetRequiredService<IConnectionFactory>(), _provider.GetRequiredService<ILoggerManager>());
            var result = publish.SendMessage(message);

            Assert.AreEqual($"Hello my name is, {message}", result);
        }
    }
}