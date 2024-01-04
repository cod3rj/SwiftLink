using MediatR;
using SwiftLink.API.Core;
using SwiftLink.API.Database;
using SwiftLink.API.Services;

namespace SwiftLink.API.Features.Url
{
    public static class Create
    {
        public record Command(Request data) : IRequest<Result<string>>;

        public class Request
        {
            public string OriginalUrl { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly DataContext _context;
            private readonly UrlService _urlService;

            public Handler(DataContext context, UrlService urlService)
            {
                _context = context;
                _urlService = urlService;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Log the received command for debugging
                Console.WriteLine($"Received command: {request.data}");

                // Validate request.OriginalUrl
                if (string.IsNullOrEmpty(request.data.OriginalUrl))
                {
                    return Result<string>.Failure("Invalid original URL.");
                }

                try
                {
                    // Log to check if _urlService is not null
                    Console.WriteLine($"_urlService is {_urlService}");

                    // Call the UrlService to create the short URL and save it to the database
                    var shortUrl = await _urlService.CreateShortUrl(request.data.OriginalUrl);

                    // Include the short URL in the success result
                    return Result<string>.Success(shortUrl);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine($"Exception: {ex}");
                    return Result<string>.Failure($"Failed to create URL. {ex.Message}");
                }
            }

            public class UnitResult
            {
                public string ShortUrl { get; set; }
            }
        }
    }
}
