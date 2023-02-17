using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetDetails
{
    public class GetUrlDetailsQueryHandler : 
        IRequestHandler<GetUrlDetailsQuery, GetUrlDetailsQueryDto>
    {
        private readonly IUrlDbContext _context;

        public GetUrlDetailsQueryHandler(IUrlDbContext context)
        {
            _context = context;
        }
        public async Task<GetUrlDetailsQueryDto> Handle
            (GetUrlDetailsQuery request, CancellationToken cancellationToken)
        {
            var url = await _context.Urls
                .FirstOrDefaultAsync(u => u.Id == request.Id
                            && u.UserId == request.UserId, cancellationToken);
            if (url == null || url.IsDeleted)
            {
                throw new NotFoundException(nameof(Url), url.Id);
            }

            return new GetUrlDetailsQueryDto()
            {
                Id = url.Id,
                Title = url.Title,
                Description = url.Description,
                BaseUri = url.BaseUri,
                CreationDate = url.CreationDate,
                EditDate = url.EditDate,
                ShortenedUri = url.ShortenedUri,
                UriShortenedPart = url.UriShortPart,
                UserId = url.UserId
            };
        }
    }
}
