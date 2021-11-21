// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Personas.Persistence
{

    public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
    {
        public PersistenceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;Password=mysecretpassword");
            return new PersistenceContext(optionsBuilder.Options);
        }
    }
}
