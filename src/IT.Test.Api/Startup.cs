// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using IT.Test.Api.HealthCheck;
using IT.Test.Bus;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace IT.Test.Api
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(log => log.AddSerilog());
            services.AddControllers()
                .AddFluentValidation(v => v.DisableDataAnnotationsValidation = true)
                .AddJsonOptions(json =>
                  {
                      JsonSerializerOptions options = json.JsonSerializerOptions;
                      options.WriteIndented = true;
                      options.Converters.Add(new JsonStringEnumConverter());
                      options.Converters.Add(new TimeSpanToStringConverter());
                      options.Converters.Add(new ExceptionToStringConverter());
                  });
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
            });
            services.AddHealthChecks();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqSettings settings = Configuration.GetSection("RMQ").Get<RabbitMqSettings>();
                    cfg.Host(settings.Host, settings.Port, settings.VirtualHost, settings.ConnectionName, host =>
                    {
                        host.Username(settings.Username);
                        host.Password(settings.Password);
                    });
                    cfg.ConnectPublishObserver(context.GetRequiredService<IPublishObserver>());
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();
            services.AddBus();
            services.AddWebApp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IT.Test.Api");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks();
            });
        }
    }
}
