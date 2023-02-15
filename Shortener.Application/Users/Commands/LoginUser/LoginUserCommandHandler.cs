using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Users.Commands.LoginUser
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ClaimsIdentity>
    {
        private readonly IUrlDbContext _context;

        public LoginUserCommandHandler(IUrlDbContext context)
        {
            _context = context;
        }

        public async Task<ClaimsIdentity> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l => l.Login == request.Login
                                                               && l.Password == request.Password, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            return claimsIdentity;
        }
    }
}
