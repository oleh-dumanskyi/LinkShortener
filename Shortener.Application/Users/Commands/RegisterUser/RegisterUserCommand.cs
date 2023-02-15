using MediatR;
using Shortener.Domain;

namespace Shortener.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IList<Url> Urls { get; set; } = new List<Url>();
    }
}
