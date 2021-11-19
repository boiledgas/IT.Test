// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Api.Models;
using IT.Test.Bus.Message;

namespace IT.Test.Api.Mappers
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<User, UserCreate>();
        }
    }
}
