using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Shortener.Application.Urls.Commands.EditUrl
{
    public class EditUrlCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Заголовок занадто довгий")]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "Опис занадто довгий")]
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Url, ErrorMessage = "Некоректні дані")]
        public Uri BaseUri { get; set; }
        [Required]
        public Uri ShortenedUri { get; set; }
        public string ShortenedPartUri { get; set; }
        public DateTime EditDate { get; set; }
    }
}