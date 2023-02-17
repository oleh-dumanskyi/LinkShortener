using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Urls.Commands.EditUrl
{
    public class EditUrlCommandHandler : IRequestHandler<EditUrlCommand, Unit>
    {
        private readonly IUrlDbContext _context;
        private const string InvalidCharacters = ".~:/?#[]@!$&'()*+,;=";
        public EditUrlCommandHandler(IUrlDbContext context) => _context = context;
        public async Task<Unit> Handle(EditUrlCommand request, CancellationToken cancellationToken)
        {
            var url = await _context.Urls.FirstOrDefaultAsync(url =>
                url.Id == request.Id && url.UserId == request.UserId, cancellationToken);

            if (url == null || url.IsDeleted)
                throw new NotFoundException(nameof(Url), url.Id);

            if (await _context.Urls.FirstOrDefaultAsync(u => u.UriShortPart == request.ShortenedPartUri 
                                                             && request.ShortenedPartUri != url.UriShortPart) != null)
                throw new ConstraintException();

            if (request.ShortenedPartUri.IndexOfAny(InvalidCharacters.ToCharArray()) != -1)
                throw new ArgumentException();

            url.Title = request.Title;
            url.Description = request.Description;
            url.BaseUri = request.BaseUri;
            url.UriShortPart = request.ShortenedPartUri;
            url.ShortenedUri =
                new Uri(request.ShortenedUri.GetLeftPart(UriPartial.Authority) + $"/{request.ShortenedPartUri}");
            url.EditDate = DateTime.Now;

            _context.Urls.Update(url);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
