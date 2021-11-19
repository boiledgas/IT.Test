// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Threading.Tasks;
using AutoMapper;
using IT.Test.Api.Models;
using IT.Test.Bus.Message;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace IT.Test.Api.Controllers
{

    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        readonly IPublishEndpoint _endpoint;
        readonly IMapper _mapper;
        public UsersController(IPublishEndpoint endpoint, IMapper mapper)
        {
            _endpoint = endpoint;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            UserCreate message = _mapper.Map<UserCreate>(user);
            await _endpoint.Publish(message);
            return Ok();
        }
    }
}
