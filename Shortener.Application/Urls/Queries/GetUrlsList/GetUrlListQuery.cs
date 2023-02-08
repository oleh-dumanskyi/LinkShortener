using MediatR;

namespace Shortener.Application.Urls.Queries.GetUrlsList
{
    public class GetUrlListQuery : IRequest<UrlsListVm>
    {
        public Guid UserId { get; set; }
    }
}
