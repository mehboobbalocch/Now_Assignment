using Application.Interfaces;
using Application.Wrappers;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateUserCommandHandler(
            IApplicationDbContext context,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid UserName.");
            }

            var passwordVerificationResult = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == request.Username && u.Password == request.Password, cancellationToken);
            if (passwordVerificationResult == null)
            {
                throw new UnauthorizedAccessException("Invalid Password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticateUserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }
    }
}
