using AutoMapper;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Users.Commands.LoginUser;

namespace Shortener.WebApi.Models
{
    public class LoginUserDto : IMapWith<LoginUserCommand>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginUserDto, LoginUserCommand>()
                .ForMember(command => command.Login, opt => opt.MapFrom(urlDto => urlDto.Login))
                .ForMember(command => command.Password, opt => opt.MapFrom(urlDto => urlDto.Password));
        }
    }
}
