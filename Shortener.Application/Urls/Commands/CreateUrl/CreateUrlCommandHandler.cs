using MediatR;
using Shortener.Application.Interfaces;
using Shortener.Domain;
using System.Text;

namespace Shortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommandHandler
        : IRequestHandler<CreateUrlCommand, Guid>
    {
        private readonly IUrlDbContext _context;

        public CreateUrlCommandHandler(IUrlDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            var ShortUrlData = CreateShortUrl(request);
            var url = new Url
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                CreationDate = DateTime.Now,
                EditDate = null,
                BaseUri = request.BaseUri,
                ShortenedUri = ShortUrlData.Item1,
                UriShortPart = ShortUrlData.Item2
            };

            await _context.Urls.AddAsync(url, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return url.Id;
        }

        private (Uri,string) CreateShortUrl(CreateUrlCommand request)
        {
            var rngNumberHash = Random.Shared.NextInt64()
                .GetHashCode().ToString();
            var base64EncodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes(rngNumberHash));
            return (new Uri(@$"{request.CurrentUri.GetLeftPart(UriPartial.Authority)}/{base64EncodedString}"),
                base64EncodedString);
        }
    }
}