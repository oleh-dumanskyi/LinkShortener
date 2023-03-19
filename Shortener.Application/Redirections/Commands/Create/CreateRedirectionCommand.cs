using MediatR;
using System.Net;

namespace Shortener.Application.Redirections.Commands.Create
{
    public class CreateRedirectionCommand : IRequest<Unit>
    {
        public string UserAgent { get; set; }
        public Guid UrlId { get; set; }
        public IPAddress IpAddress { get; set; }
        public string Token { get; set; }
    }
}
