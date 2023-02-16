using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Urls.Commands.CreateUrl;

namespace Shortener.WebApi.Models
{
    public class CreateUrlDto : IMapWith<CreateUrlCommand>
    {
        [Required(ErrorMessage = "Заголовок не введений")]
        [MaxLength(250, ErrorMessage = "Заголовок задовгий")]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "Опис задовгий")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Посилання не введено")]
        [DataType(DataType.Url, ErrorMessage = "Введіть повну URL адресу")]
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
