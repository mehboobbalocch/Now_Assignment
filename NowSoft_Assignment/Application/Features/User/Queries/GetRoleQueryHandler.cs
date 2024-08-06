using Application.Interfaces;
using Application.Wrappers;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Queries
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RoleModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetRoleQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleModel>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {

            var roles = await _context.Roles
                .Select(r => new RoleModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                })
                .ToListAsync(cancellationToken);

            return roles;
        }
    }
}
