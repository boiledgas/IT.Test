// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IT.Test.Bus.Logging
{
    public class PublishObserver : IPublishObserver
    {
        readonly ILogger<PublishObserver> _logger;
        public PublishObserver(ILogger<PublishObserver> logger)
        {
            _logger = logger;
        }

        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogInformation("message sent: {@Message}", context.Message);
            return Task.CompletedTask;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
