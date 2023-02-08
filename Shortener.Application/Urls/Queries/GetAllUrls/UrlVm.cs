using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Domain;

namespace Shortener.Application.Urls.Queries.GetAllUrls
{
    public class UrlVm : IMapWith<Url>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public Uri BaseUri { get; set; }
        public Uri ShortenedUri { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(url => url.Id));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.Title,
                    opt => opt.MapFrom(url => url.Title));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.Description,
                    opt => opt.MapFrom(url => url.Description));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.CreationDate,
                    opt => opt.MapFrom(url => url.CreationDate));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.EditDate,
                    opt => opt.MapFrom(url => url.EditDate));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.BaseUri,
                    opt => opt.MapFrom(url => url.BaseUri));
            profile.CreateMap<Url, UrlVm>()
                .ForMember(dto => dto.ShortenedUri,
                    opt => opt.MapFrom(url => url.ShortenedUri));
        }
    }
}
