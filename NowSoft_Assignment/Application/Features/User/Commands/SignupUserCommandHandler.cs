using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Application.Features.User.Commands
{
    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SignupUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _context.AspNetUsers.AnyAsync(user => user.Email == request.Email);

            if (emailExists)
            {
                throw new EmailAlreadyExistsException(request.Email);
            }

            var user = _mapper.Map<SignupUserCommand, ApplicationUser>(request);
            
            
            var userRoles = request.Roles.Select(roleId => new ApplicationUserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = roleId
            }).ToList();
            user.UserRoles = userRoles;
            user.Balance = 8.0;
            await _context.AspNetUsers.AddAsync(user, cancellationToken);
            await _context.UserRoles.AddRangeAsync(userRoles, cancellationToken);
            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}
