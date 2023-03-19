using MediatR;

namespace Shortener.Application.Redirections.Queries.GetStatistics
{
    public class GetStatisticsQuery : IRequest<StatisticsDto>
    {
        public Guid UrlId { get; set; }
        public StatisticType StatisticType { get; set; }
    }

    public enum StatisticType
    {
        ByDate,
        ByCountry,
        ByOS,
        ByBrowser
    }
}
