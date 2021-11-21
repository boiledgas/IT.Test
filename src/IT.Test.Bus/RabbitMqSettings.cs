// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Web;

namespace IT.Test.Bus
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string ConnectionName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Uri Uri => new Uri($"amqp://{HttpUtility.UrlEncode(Username)}:{HttpUtility.UrlEncode(Password)}@{Host}:{Port}/{HttpUtility.UrlEncode(VirtualHost)}");
    }
}
