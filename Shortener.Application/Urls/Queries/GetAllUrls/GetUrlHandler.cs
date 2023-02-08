using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetAllUrls
{
    public class GetUrlHandler : IRequestHandler<GetUrlQuery, UrlVm>
    {
        private readonly IUrlDbContext _context;
        private readonly IMapper _mapper;

        public GetUrlHandler(IUrlDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UrlVm> Handle(GetUrlQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Urls.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId == request.UserId || entity.IsDeleted)
                throw new NotFoundException(nameof(Url), request.Id);

            return _mapper.Map<UrlVm>(entity);
        }
    }
}
