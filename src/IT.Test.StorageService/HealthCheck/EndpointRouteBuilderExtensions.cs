// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IT.Test.StorageService.HealthCheck
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/masstransit-bus", new HealthCheckOptions
            {
                Predicate = check => check.Name.Equals("masstransit-bus", StringComparison.OrdinalIgnoreCase)
            });
            endpoints.MapHealthChecks("/health/postgres", new HealthCheckOptions
            {
                Predicate = check => check.Name.Equals("postgres", StringComparison.OrdinalIgnoreCase)
            });
            _ = endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (ctx, report) =>
                {
                    JsonOptions options = ctx.RequestServices.GetRequiredService<IOptions<JsonOptions>>().Value;
                    string json = JsonSerializer.Serialize(report, options.JsonSerializerOptions);

                    ctx.Response.StatusCode = 200;
                    ctx.Response.ContentType = "application/json";
                    await ctx.Response.WriteAsync(json, Encoding.UTF8);
                }
            });
            return endpoints;
        }
    }
}
