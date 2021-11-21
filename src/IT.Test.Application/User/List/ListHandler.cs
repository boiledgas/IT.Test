// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IT.Test.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IT.Test.Application.User.List
{
    public class ListHandler : IRequestHandler<ListQuery, ListResponse>
    {
        readonly PersistenceContext _context;
        public ListHandler(PersistenceContext context)
        {
            _context = context;
        }
        public async Task<ListResponse> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Persistence.Entities.User> query = _context.Users.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(request.Organization))
                query = query.Where(u => u.Organization.Name == request.Organization);

            int count = await query.CountAsync();

            IList<Persistence.Entities.User> users = await query
                .OrderBy(u => u.Id)
                .Skip(request.Offset)
                .Take(request.Count)
                .ToListAsync();

            return new ListResponse(users, count);
        }
    }
}
