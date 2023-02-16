using System.ComponentModel.DataAnnotations;
using MediatR;
using Shortener.Domain;

namespace Shortener.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Не вказане ім'я користувача")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Не вказаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IList<Url> Urls { get; set; } = new List<Url>();
    }
}
