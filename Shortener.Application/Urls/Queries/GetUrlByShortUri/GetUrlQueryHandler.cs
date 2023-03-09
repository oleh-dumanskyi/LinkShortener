using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetUrlByShortUri
{
    public class GetUrlQueryHandler : IRequestHandler<GetUrlQuery, UrlVm>
    {
        private readonly IUrlDbContext _context;
        private readonly IMapper _mapper;

        public GetUrlQueryHandler(IUrlDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UrlVm> Handle(GetUrlQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Urls.FirstOrDefaultAsync(u => u.UriShortPart == request.UriShortenedPart, cancellationToken);

            if (entity == null || entity.IsDeleted)
                throw new NotFoundException(nameof(Url), request.Id);

            entity.FollowingsCounter += 1;
            _context.Urls.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UrlVm>(entity);
        }
    }
}
