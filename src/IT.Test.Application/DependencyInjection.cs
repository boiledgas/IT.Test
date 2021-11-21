// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Reflection;
using FluentValidation;
using IT.Test.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Test.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
