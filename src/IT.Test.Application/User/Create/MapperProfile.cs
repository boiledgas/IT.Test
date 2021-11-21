// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Bus.Message;

namespace IT.Test.Application.User.Create
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreate, CreateCommand>();
            CreateMap<CreateCommand, Persistence.Entities.User>();
        }
    }
}
