// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Threading;
using System.Threading.Tasks;
using IT.Test.Application.Exceptions;
using IT.Test.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IT.Test.Application.User.SetOrganization
{
    public class SetOrganizationHandler : IRequestHandler<SetOrganizationCommand, Unit>
    {
        readonly PersistenceContext _context;
        public SetOrganizationHandler(PersistenceContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(SetOrganizationCommand request, CancellationToken cancellationToken)
        {
            Persistence.Entities.User user = await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(request.Email));
            if (user == null)
                throw new UserNotFoundException(request.Email);

            Persistence.Entities.Organization organization = await _context.Organizations.SingleOrDefaultAsync(u => u.Name.Equals(request.OrganizationName));
            if (organization == null)
            {
                organization = new Persistence.Entities.Organization(request.OrganizationName);
                _context.Organizations.Add(organization);
            }

            user.SetOrganization(organization);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
