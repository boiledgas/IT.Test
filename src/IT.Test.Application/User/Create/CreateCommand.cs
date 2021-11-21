// Тестовое задание https://github.com/boiledgas/IT.Test

using MediatR;

namespace IT.Test.Application.User.Create
{
    public class CreateCommand : IRequest<CreateResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
    }
}
