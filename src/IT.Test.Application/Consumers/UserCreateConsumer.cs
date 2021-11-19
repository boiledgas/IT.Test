// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Threading.Tasks;
using AutoMapper;
using IT.Test.Application.User.Create;
using IT.Test.Bus.Message;
using MassTransit;
using MediatR;

namespace IT.Test.Application.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreate>
    {
        readonly IMapper _mapper;
        readonly IMediator _mediatr;
        public UserCreateConsumer(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserCreate> context)
        {
            CreateCommand command = _mapper.Map<CreateCommand>(context.Message);
            await _mediatr.Send(command);
        }
    }
}
