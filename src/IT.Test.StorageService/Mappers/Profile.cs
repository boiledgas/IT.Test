// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Application.User.List;
using IT.Test.Application.User.SetOrganization;
using IT.Test.StorageService.Models;

namespace IT.Test.StorageService.Mappers
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<ListRequest, ListQuery>();
            CreateMap<SetOrganizationRequest, SetOrganizationCommand>();
            CreateMap<ListResponse, PaginationResponse<User>>()
                .ForMember(p => p.Data, opt => opt.MapFrom(l => l.Users));
            CreateMap<Persistence.Entities.User, User>()
                .ForMember(u => u.Organization, opt => opt.MapFrom(e => e.Organization != null ? e.Organization.Name : null));
        }
    }
}
