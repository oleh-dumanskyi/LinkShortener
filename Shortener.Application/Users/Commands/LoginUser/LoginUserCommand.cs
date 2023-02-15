using System.Security.Claims;
using MediatR;

namespace Shortener.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ClaimsIdentity>
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
