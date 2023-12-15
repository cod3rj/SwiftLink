using FluentValidation;
using MediatR;
using SwiftLink.API.Core;
using SwiftLink.API.Database;
using SwiftLink.API.Services;

namespace SwiftLink.API.Features.Url
{
    public static class Create
    {
        public record Command(Request data) : IRequest<Result<Unit>>;

        public class Request
        {
            public string OriginalUrl { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly UrlService _urlService;

            public Handler(DataContext context, UrlService urlService)
            {
                _context = context;
                _urlService = urlService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Log the received command for debugging
                Console.WriteLine($"Received command: {request.data}");

                // Validate request.OriginalUrl
                if (string.IsNullOrEmpty(request.data.OriginalUrl))
                {
                    return Result<Unit>.Failure("Invalid original URL.");
                }

                try
                {
                    // Log to check if _urlService is not null
                    Console.WriteLine($"_urlService is {_urlService}");

                    // Call the UrlService to create the short URL and save it to the database
                    var shortUrl = await _urlService.CreateShortUrl(request.data.OriginalUrl);

                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine($"Exception: {ex}");
                    return Result<Unit>.Failure($"Failed to create URL. {ex.Message}");
                }
            }
        }
    }
}
