using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Users.Commands.LoginUser;

namespace Shortener.WebApi.Models
{
    public class LoginUserDto : IMapWith<LoginUserCommand>
    {
        [Required(ErrorMessage = "Не вказане ім'я користувача")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Не вказаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginUserDto, LoginUserCommand>()
                .ForMember(command => command.Login, opt => opt.MapFrom(urlDto => urlDto.Login))
                .ForMember(command => command.Password, opt => opt.MapFrom(urlDto => urlDto.Password));
        }
    }
}
