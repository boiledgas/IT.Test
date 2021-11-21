// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IT.Test.Application.Exceptions;
using IT.Test.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IT.Test.Application.User.Create
{
    public class CreateHandler : IRequestHandler<CreateCommand, CreateResponse>
    {
        readonly PersistenceContext _context;
        readonly IMapper _mapper;
        public CreateHandler(PersistenceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            bool exists = await _context.Users.Where(u => u.Email == request.Email).AnyAsync();
            if (exists)
                throw new UserExistException(request.Email);

            Persistence.Entities.User user = _mapper.Map<Persistence.Entities.User>(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new CreateResponse
            {
                Id = user.Id
            };
        }
    }
}
