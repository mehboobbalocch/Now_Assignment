using MediatR;

namespace Application.Features.User.Commands
{
    public class SignupUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public List<Guid> Roles { get; set; }
    }
}
