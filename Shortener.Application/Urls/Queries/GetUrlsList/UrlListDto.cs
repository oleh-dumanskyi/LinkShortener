using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetUrlsList
{
    public class UrlListDto : IMapWith<Url>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Url, UrlListDto>()
                .ForMember(urlDto => urlDto.Id,
                    opt => opt.MapFrom(url => url.Id))
                .ForMember(urlDto => urlDto.Title,
                    opt => opt.MapFrom(url => url.Title));
        }
    }
}
