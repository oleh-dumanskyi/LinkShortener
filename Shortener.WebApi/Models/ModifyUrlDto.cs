using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Urls.Commands.DeleteUrl;
using Shortener.Application.Urls.Queries.GetDetails;

namespace Shortener.WebApi.Models
{
    public class ModifyUrlDto : IMapWith<DeleteUrlCommand>, IMapWith<GetUrlDetailsQuery>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModifyUrlDto, DeleteUrlCommand>()
                .ForMember(command => command.Id,
                    opt => opt.MapFrom(urlDto => urlDto.Id))
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(urlDto => urlDto.UserId));
            profile.CreateMap<ModifyUrlDto, GetUrlDetailsQuery>()
                .ForMember(command => command.Id,
                    opt => opt.MapFrom(urlDto => urlDto.Id))
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(urlDto => urlDto.UserId));
        }
    }
}
