using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Domain;

namespace Shortener.Application.Redirections.Queries.GetStatistics
{
    public class StatisticsDto : IMapWith<Redirection>
    {
        public Dictionary<string, int>? CountryStatistics { get; set; } = new Dictionary<string, int>();
        //public string? CountryName { get; set; }
        //public int CountryGroupCount { get; set; }
        public Dictionary<DateTime, int>? DateStatistics { get; set; } = new Dictionary<DateTime, int>();
        //public DateTime? Date { get; set; }
        //public int DateGroupCount { get; set; }
        public Dictionary<string, int>? OsStatistics { get; set; } = new Dictionary<string, int>();
        //public string? OperatingSystem { get; set; }
        //public int OSGroupCount { get; set; }
        public Dictionary<string, int>? BrowserStatistics { get; set; } = new Dictionary<string, int>();
        //public string? Browser { get; set; }
        //public int BrowserGroupCount { get; set; }

        /*public void Mapping(Profile profile)
        {
            profile.CreateMap<Redirection, StatisticsDto>()
                .ForMember(statisticsDto => statisticsDto.CountryStatistics.Keys,
                    opt => opt.MapFrom(r => r.CountryName))
                .ForMember(statisticsDto => statisticsDto.DateStatistics.Keys,
                    opt => opt.MapFrom(r => r.Date))
                .ForMember(statisticsDto => statisticsDto.OsStatistics.Keys,
                    opt => opt.MapFrom(r => r.OperatingSystem))
                .ForMember(statisticsDto => statisticsDto.BrowserStatistics.Keys,
                    opt => opt.MapFrom(r => r.Browser));
        }*/
    }
}
