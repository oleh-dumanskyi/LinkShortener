using MediatR;
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
                _context.Urls.FirstOrDefault(u => u.Id == request.Id);

            if (entity == null || entity.UserId != request.UserId)
                throw new NotFoundException(nameof(Url), request.Id);

            _context.Urls.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}