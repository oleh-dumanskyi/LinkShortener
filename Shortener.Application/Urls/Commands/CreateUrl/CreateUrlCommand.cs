using MediatR;

namespace Shortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri BaseUri { get; set; }
        public Uri CurrentUri { get; set; }
    }
}
