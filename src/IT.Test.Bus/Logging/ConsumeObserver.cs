// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IT.Test.Bus.Logging
{
    public class ConsumeObserver : IConsumeObserver
    {
        readonly ILogger<ConsumeObserver> _logger;
        public ConsumeObserver(ILogger<ConsumeObserver> logger)
        {
            _logger = logger;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            _logger.LogInformation("message processed {@Message}", context.Message);
            return Task.CompletedTask;
        }

        public Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
