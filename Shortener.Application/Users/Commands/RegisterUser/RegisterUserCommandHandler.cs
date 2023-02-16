using MediatR;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUrlDbContext _context;
        public RegisterUserCommandHandler(IUrlDbContext context) => _context = context;
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = new Guid(),
                Login = request.Login,
                Password = request.Password,
                Urls = new List<Url>()
            };

            //if (user.Login == string.Empty || user.Password == string.Empty)
            //    throw new InvalidDataException();

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
