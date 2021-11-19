// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation.AspNetCore;
using IT.Test.Application;
using IT.Test.Application.Consumers;
using IT.Test.Bus;
using IT.Test.Model;
using IT.Test.Model.Configuration;
using IT.Test.Shared;
using IT.Test.StorageService.Filters;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            }).AddFluentValidation(v => v.DisableDataAnnotationsValidation = true);
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreateConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqSettings settings = Configuration.GetSection("RMQ").Get<RabbitMqSettings>();
                    cfg.Host(settings.Host, settings.Port, settings.VirtualHost, settings.ConnectionName, host =>
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

            PersistenceConfiguration config = Configuration.GetSection("DB").Get<PersistenceConfiguration>();
            services.AddPersistence(config);
            services.AddApplication();
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
            });
        }
    }
}
