using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Interfaces;

namespace Shortener.Application.Urls.Queries.GetUrlsList
{
    public class GetUrlListQueryHandler : IRequestHandler<GetUrlListQuery, UrlsListVm>
    {
        private readonly IUrlDbContext _context;
        private readonly IMapper _mapper;

        public GetUrlListQueryHandler(IUrlDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UrlsListVm> Handle(GetUrlListQuery request, CancellationToken cancellationToken)
        {
            var urlsQuery = await _context.Urls
                .Where(url => url.UserId == request.UserId && !url.IsDeleted)
                .ProjectTo<UrlListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UrlsListVm { Urls = urlsQuery };
        }
    }
}
