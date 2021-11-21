// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Threading;
using System.Threading.Tasks;
using IT.Test.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IT.Test.StorageService
{

    public class ProgramStartup : IHostedService
    {
        readonly ILogger<ProgramStartup> _logger;
        readonly IServiceScopeFactory _scopeFactory;

        public ProgramStartup(ILogger<ProgramStartup> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    PersistenceContext database = scope.ServiceProvider.GetRequiredService<PersistenceContext>();

                    await database.MigrateAsync(cancellationToken);
                    _logger.LogInformation("Database migrated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Startup error");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
