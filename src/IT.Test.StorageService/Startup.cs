// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using IT.Test.Application;
using IT.Test.Application.Consumers;
using IT.Test.Bus;
using IT.Test.Model;
using IT.Test.Model.Configuration;
using IT.Test.StorageService.Filters;
using IT.Test.StorageService.HealthCheck;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace IT.Test.StorageService
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
            services.AddLogging(builder => builder.AddSerilog());

            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
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
                x.AddConsumer<UserCreateConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqSettings settings = Configuration.GetSection("RMQ").Get<RabbitMqSettings>();
                    cfg.Host(settings.Uri, settings.ConnectionName, host =>
                    {
                        host.Username(settings.Username);
                        host.Password(settings.Password);
                    });
                    cfg.ConnectConsumeObserver(context.GetRequiredService<IConsumeObserver>());
                    cfg.ReceiveEndpoint("user-create", e =>
                    {
                        e.SetQuorumQueue();
                        e.ConfigureConsumer<UserCreateConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddHostedService<ProgramStartup>();
            services.AddBus();

            PersistenceConfiguration config = Configuration.GetSection("DB").Get<PersistenceConfiguration>();
            services.AddPersistence(config);
            services.AddApplication();
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
