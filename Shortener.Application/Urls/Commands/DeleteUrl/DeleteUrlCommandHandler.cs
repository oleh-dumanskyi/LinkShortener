using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Interfaces;
using Shortener.Domain;

namespace Shortener.Application.Urls.Commands.DeleteUrl
{
    public class DeleteUrlCommandHandler : IRequestHandler<DeleteUrlCommand>
    {
        private readonly IUrlDbContext _context;

        public DeleteUrlCommandHandler(IUrlDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUrlCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _context.Urls.FindAsync(new object[] {request.Id}, cancellationToken);

            if (entity == null || entity.UserId != request.UserId || entity.IsDeleted)
                throw new NotFoundException(nameof(Url), request.Id);

            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
