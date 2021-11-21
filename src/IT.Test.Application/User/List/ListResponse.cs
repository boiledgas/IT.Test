// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Collections.Generic;

namespace IT.Test.Application.User.List
{
    public class ListResponse
    {
        public IList<Persistence.Entities.User> Users { get; }
        public int Count { get; }

        public ListResponse(IList<Persistence.Entities.User> users, int count)
        {
            Users = users;
            Count = count;
        }
    }
}
