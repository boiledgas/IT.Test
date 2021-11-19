// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Threading.Tasks;
using AutoMapper;
using IT.Test.Application.User.List;
using IT.Test.Application.User.SetOrganization;
using IT.Test.StorageService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IT.Test.Api.Controllers
{


    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IMediator _mediatr;
        public UsersController(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("setOrganization")]
        public Task SetOrganization([FromBody] SetOrganizationRequest request)
        {
            SetOrganizationCommand command = _mapper.Map<SetOrganizationCommand>(request);
            return _mediatr.Send(command);
        }

        [HttpPost]
        [Route("list")]
        public async Task<PaginationResponse<User>> List([FromBody] ListRequest request)
        {
            ListQuery query = _mapper.Map<ListQuery>(request);
            ListResponse list = await _mediatr.Send(query);
            return _mapper.Map<PaginationResponse<User>>(list);
        }
    }
}
