using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Urls.Commands.DeleteUrl;

namespace Shortener.WebApi.Models
{
    public class ModifyUrlDto : IMapWith<DeleteUrlCommand>
    {
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModifyUrlDto, DeleteUrlCommand>()
                .ForMember(command => command.Id, opt => opt.MapFrom(urlDto => urlDto.Id));
        }
    }
}
