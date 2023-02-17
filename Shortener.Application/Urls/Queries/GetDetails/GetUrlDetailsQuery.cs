using MediatR;

namespace Shortener.Application.Urls.Queries.GetDetails
{
    public class GetUrlDetailsQuery : IRequest<GetUrlDetailsQueryDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
