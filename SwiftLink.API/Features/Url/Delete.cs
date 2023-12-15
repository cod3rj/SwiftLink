using MediatR;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Url
{
    public static class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int UrlId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var url = await _context.Urls.FirstOrDefaultAsync(x => x.Id == request.UrlId);

                if (url == null)
                {
                    return Result<Unit>.Failure("Url not found.");
                }

                _context.Remove(url);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to delete url.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}