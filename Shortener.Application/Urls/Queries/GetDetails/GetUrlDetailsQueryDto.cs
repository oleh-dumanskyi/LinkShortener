using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetDetails
{
    public class GetUrlDetailsQueryDto : IMapWith<Url>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public Uri BaseUri { get; set; }
        public Uri ShortenedUri { get; set; }
        public string UriShortenedPart { get; set; }
        public long FollowingsCounter { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetUrlDetailsQueryDto, Url>()
                .ForMember(urlDto => urlDto.Id,
                    opt => opt.MapFrom(url => url.Id))
                .ForMember(urlDto => urlDto.UserId,
                    opt => opt.MapFrom(url => url.UserId))
                .ForMember(urlDto => urlDto.Title,
                    opt => opt.MapFrom(url => url.Title))
                .ForMember(urlDto => urlDto.Description,
                    opt => opt.MapFrom(url => url.Description))
                .ForMember(urlDto => urlDto.BaseUri,
                    opt => opt.MapFrom(url => url.BaseUri))
                .ForMember(urlDto => urlDto.ShortenedUri,
                    opt => opt.MapFrom(url => url.ShortenedUri))
                .ForMember(urlDto => urlDto.CreationDate,
                    opt => opt.MapFrom(url => url.CreationDate))
                .ForMember(urlDto => urlDto.EditDate,
                    opt => opt.MapFrom(url => url.EditDate))
                .ForMember(urlDto => urlDto.UriShortPart,
                    opt => opt.MapFrom(url => url.UriShortenedPart))
                .ForMember(urlDto => urlDto.FollowingsCounter,
                    opt => opt.MapFrom(url => url.FollowingsCounter));
        }
    }
}
