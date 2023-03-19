using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Interfaces;

namespace Shortener.Application.Redirections.Queries.GetStatistics
{
    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatisticsDto>
    {
        private readonly IUrlDbContext _context;

        public GetStatisticsQueryHandler(IUrlDbContext context)
        {
            _context = context;
        }
        public async Task<StatisticsDto> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            StatisticsDto dto = new StatisticsDto();
            if (request.StatisticType == StatisticType.ByCountry)
            {
                var groupedRedirections = await _context.Redirections
                    .Where(id => id.UrlId == request.UrlId)
                    .Select(c => new { c.CountryName })
                    .ToListAsync(cancellationToken);
                foreach (var element in groupedRedirections.Where(u=>u.CountryName != null).GroupBy(c => c.CountryName))
                {
                    dto.CountryStatistics.Add(element.Key, element.Count());
                }
            }
            else if (request.StatisticType == StatisticType.ByDate)
            {
                var groupedRedirections = await _context.Redirections
                    .Where(id => id.UrlId == request.UrlId)
                    .Select(c => new {c.Date})
                    .ToListAsync(cancellationToken);
                foreach (var element in groupedRedirections.GroupBy(c => c.Date))
                {
                    dto.DateStatistics.Add(element.Key, element.Count());
                }
            }
            else if (request.StatisticType == StatisticType.ByBrowser)
            {
                var groupedRedirections = await _context.Redirections
                    .Where(id => id.UrlId == request.UrlId)
                    .Select(c => new {c.Browser})
                    .ToListAsync(cancellationToken);
                foreach (var element in groupedRedirections.GroupBy(c => c.Browser))
                {
                    dto.BrowserStatistics.Add(element.Key, element.Count());
                }
            }
            else if (request.StatisticType == StatisticType.ByOS)
            {
                var groupedRedirections = await _context.Redirections
                    .Where(id => id.UrlId == request.UrlId)
                    .Select(c => new { c.OperatingSystem })
                    .ToListAsync();
                foreach (var element in groupedRedirections.GroupBy(c => c.OperatingSystem))
                {
                    dto.OsStatistics.Add(element.Key, element.Count());
                }
            }

            return dto;
        }
    }
}
