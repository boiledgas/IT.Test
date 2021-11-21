// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation;
using FluentValidation.Results;
using IT.Test.Model.Configuration;
using IT.Test.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Test.Model
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, PersistenceConfiguration config)
        {
            var validator = new PersistenceConfigurationValidator();
            ValidationResult validationResult = validator.Validate(config);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            services
                .AddDbContext<PersistenceContext>(options =>
                {
                    options.UseNpgsql(config.ConnectionString);
                });

            services
                .AddHealthChecks()
                .AddNpgSql(config.ConnectionString, name: "postgres");

            return services;
        }
    }
}
