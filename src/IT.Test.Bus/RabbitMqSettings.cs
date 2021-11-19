// Тестовое задание https://github.com/boiledgas/IT.Test

namespace IT.Test.Shared
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string ConnectionName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
