using MediatR;

namespace Shortener.Application.Urls.Queries.GetAllUrls
{
    public class GetUrlQuery : IRequest<UrlVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
