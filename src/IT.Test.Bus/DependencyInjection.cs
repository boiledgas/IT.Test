// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Bus.Logging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Test.Bus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<IPublishObserver, PublishObserver>();
            services.AddSingleton<IConsumeObserver, ConsumeObserver>();

            return services;
        }
    }
}
