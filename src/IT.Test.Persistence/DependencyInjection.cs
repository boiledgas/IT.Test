// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            PersistenceConfigurationValidator validator = Activator.CreateInstance<PersistenceConfigurationValidator>();

            FluentValidation.Results.ValidationResult validationResult = validator.Validate(config);
            if (!validationResult.IsValid)
            {
                string validationFields = string.Join(", ", validationResult.Errors.Select(e => e.PropertyName));
                throw new ValidationException($"Missing configuration fields: {validationFields}");
            }

            services
                .AddDbContext<PersistenceContext>(options =>
                {
                    options.UseNpgsql(config.ConnectionString);
                })
                .AddHealthChecks()
                .AddNpgSql(config.ConnectionString, name: "db");

            return services;
        }
    }
}
