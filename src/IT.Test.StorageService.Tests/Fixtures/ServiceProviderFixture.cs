// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Threading.Tasks;
using IT.Test.Application;
using IT.Test.Application.Consumers;
using IT.Test.Persistence;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IT.Test.StorageService
{
    public class ServiceProviderFixture : IAsyncLifetime
    {
        SqliteConnection _connection;
        IServiceProvider _sp;

        public T Get<T>() => _sp.GetRequiredService<T>();
        public T Controller<T>()
            where T : ControllerBase
            => ActivatorUtilities.CreateInstance<T>(_sp);

        public virtual Task DisposeAsync()
            => _connection.CloseAsync();

        public virtual async Task InitializeAsync()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddApplication();
            services.AddWebApp();
            services.AddMassTransitInMemoryTestHarness(cfg =>
            {
                cfg.AddConsumer<UserCreateConsumer>();
            });

            string connectionString = "DataSource=db;mode=memory";
            _connection = new SqliteConnection(connectionString);
            _connection.Open();

            var builder = new DbContextOptionsBuilder<PersistenceContext>();
            services.AddDbContext<PersistenceContext>(builder =>
            {
                builder.UseSqlite(_connection);
            });
            _sp = services.BuildServiceProvider();

            PersistenceContext db = _sp.GetRequiredService<PersistenceContext>();
            string sql = db.Database.GenerateCreateScript();
            await db.MigrateAsync();
        }
    }
}
