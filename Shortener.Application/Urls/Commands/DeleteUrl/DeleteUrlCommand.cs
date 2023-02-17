using MediatR;

namespace Shortener.Application.Urls.Commands.DeleteUrl
{
    public class DeleteUrlCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}