using MediatR;
using System.Drawing;

namespace Shortener.Application.Urls.Commands.CreateQrCode
{
    public class CreateQrCodeCommand : IRequest<Bitmap>
    {
        public Guid UrlId { get; set; }
    }
}
