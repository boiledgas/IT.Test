// Тестовое задание https://github.com/boiledgas/IT.Test

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using IT.Test.Application.Consumers;
using IT.Test.Bus.Message;
using IT.Test.Persistence;
using IT.Test.StorageService.Fixtures;
using Xunit;
using DbUser = IT.Test.Persistence.Entities.User;

namespace IT.Test.StorageService
{
    public class UserCreateConsumerTests : IClassFixture<MasstransitConsumerFixture<UserCreateConsumer>>
    {
        readonly MasstransitConsumerFixture<UserCreateConsumer> _sp;
        public UserCreateConsumerTests(MasstransitConsumerFixture<UserCreateConsumer> sp)
            => _sp = sp;
        [Fact]
        public async Task UserCreateConsumer_UserCreate_Success()
        {
            var message = new UserCreate
            {
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Number = Guid.NewGuid().ToString(),
                Patronymic = Guid.NewGuid().ToString(),
                Surname = Guid.NewGuid().ToString()
            };
            await _sp.Send(message);

            PersistenceContext context = _sp.Get<PersistenceContext>();

            DbUser user = context.Users.Single(u => u.Email == message.Email);
            Assert.NotNull(user);
            Assert.Equal(message.Email, user.Email);
            Assert.Equal(message.Name, user.Name);
            Assert.Equal(message.Number, user.Number);
            Assert.Equal(message.Patronymic, user.Patronymic);
            Assert.Equal(message.Surname, user.Surname);
        }

        [Fact]
        public async Task UserCreateConsumer_UserCreate_Validation()
        {
            var message = new UserCreate();
            await Assert.ThrowsAsync<ValidationException>(() => _sp.Send(message));
        }
    }
}
