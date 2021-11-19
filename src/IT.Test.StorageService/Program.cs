// Тестовое задание https://github.com/boiledgas/IT.Test

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IT.Test.StorageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.AddEnvironmentVariables("IT_TEST_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog((ctx, builder) => builder.ReadFrom.Configuration(ctx.Configuration));
                });
    }
}
