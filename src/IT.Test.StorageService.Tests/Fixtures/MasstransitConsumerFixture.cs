// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Xunit;

namespace IT.Test.StorageService.Fixtures
{
    public class MasstransitConsumerFixture<T> : ServiceProviderFixture
        where T : class, IConsumer
    {
        public async Task Send<TMessage>(TMessage message)
            where TMessage : class
        {
            var harness = new InMemoryTestHarness();
            ConsumerTestHarness<T> consumer = harness.Consumer<T>(() =>
            {
                T consumer = Get<T>();
                return consumer;
            });
            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(message);

                IReceivedMessage consumedMessage = consumer.Consumed.Select(m => true).SingleOrDefault();
                Assert.NotNull(consumedMessage);
                if (consumedMessage.Exception != null)
                    throw consumedMessage.Exception;
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
