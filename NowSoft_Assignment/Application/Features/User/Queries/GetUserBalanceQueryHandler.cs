using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public class GetUserBalanceQueryHandler : IRequestHandler<GetUserBalanceQuery, UserBalanceModel>
    {
        private readonly IApplicationDbContext _context;

        public GetUserBalanceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserBalanceModel> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.AspNetUsers
                .Where(u => u.UserName == request.UserId)
                .Select(u => new UserBalanceModel
                {
                    UserId = u.Id,
                    Balance = u.Balance
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid UserName.");
            }

            return user;
        }
    }
}