// Тестовое задание https://github.com/boiledgas/IT.Test

using MediatR;

namespace IT.Test.Application.User.List
{
    public class ListQuery : IRequest<ListResponse>
    {
        public string Organization { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
