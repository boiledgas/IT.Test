// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Test.StorageService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApp(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
