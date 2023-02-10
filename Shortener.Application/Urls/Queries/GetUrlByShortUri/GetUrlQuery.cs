using MediatR;

namespace Shortener.Application.Urls.Queries.GetUrlByShortUri
{
    public class GetUrlQuery : IRequest<UrlVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UriShortenedPart { get; set; }
    }
}
