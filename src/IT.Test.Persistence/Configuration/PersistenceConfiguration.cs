// Тестовое задание https://github.com/boiledgas/IT.Test

namespace IT.Test.Model.Configuration
{
    public class PersistenceConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                string[] paramerers = new[]
                {
                    $"Host={Host}",
                    $"Port={Port}",
                    $"Database={Name}",
                    $"Username={User}",
                    $"Password={Password}",
                };

                return string.Join(";", paramerers);
            }
        }
    }
}
