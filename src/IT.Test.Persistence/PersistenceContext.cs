// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using IT.Test.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IT.Test.Persistence
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasCollation("nocase", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
        }
        public async Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            await Database.MigrateAsync(cancellationToken);
        }
        public async Task EnsureCreatedAsync(CancellationToken cancellationToken = default)
        {
            await Database.EnsureCreatedAsync(cancellationToken);
        }
        public async Task EnsureDeletedAsync(CancellationToken cancellationToken = default)
        {
            await Database.EnsureDeletedAsync(cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
