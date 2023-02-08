using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Urls.Commands.CreateUrl;

namespace Shortener.WebApi.Models
{
    public class CreateUrlDto : IMapWith<CreateUrlCommand>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri BaseUri { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUrlDto, CreateUrlCommand>()
                .ForMember(command => command.Title, opt => opt.MapFrom(urlDto => urlDto.Title))
                .ForMember(command => command.Description, opt => opt.MapFrom(urlDto => urlDto.Description))
                .ForMember(command => command.BaseUri, opt => opt.MapFrom(urlDto => urlDto.BaseUri));
        }
    }
}
