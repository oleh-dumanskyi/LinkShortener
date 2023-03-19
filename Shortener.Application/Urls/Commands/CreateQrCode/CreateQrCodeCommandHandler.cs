using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using System.Drawing;
using System.Text;
using ZXing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace Shortener.Application.Urls.Commands.CreateQrCode
{
    public class CreateQrCodeCommandHandler : IRequestHandler<CreateQrCodeCommand, Bitmap>
    {
        private readonly IUrlDbContext _context;

        public CreateQrCodeCommandHandler(IUrlDbContext context)
        {
            _context = context;
        }
        public async Task<Bitmap> Handle(CreateQrCodeCommand request, CancellationToken cancellationToken)
        {
            Bitmap bitmap;
            var url = await _context.Urls.FirstOrDefaultAsync(id => id.Id == request.UrlId, cancellationToken);
            if (url == null)
            {
                throw new NotFoundException(nameof(CreateQrCodeCommandHandler), url.Id);
            }

            if (url.QrCodeImageBytes != null)
            {
                byte[] existingBytes = Encoding.ASCII.GetBytes(url.QrCodeImageBytes);
                using (var ms = new MemoryStream(existingBytes))
                {
                    bitmap = new Bitmap(ms);
                }

                return bitmap;
            }

            var width = 250;
            var height = 250;
            var margin = 0;
            var qrCodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            
            byte[] bytes;
            bitmap = new Bitmap(qrCodeWriter.Write(url.ShortenedUri.AbsoluteUri));

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes = stream.ToArray();
            }

            url.QrCodeImageBytes = Encoding.Default.GetString(bytes);
            await _context.SaveChangesAsync(cancellationToken);

            return bitmap;
        }
    }
}
