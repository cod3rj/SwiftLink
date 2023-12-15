using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Url
{
    public static class GetRedirectUrl
    {
        public class Query : IRequest<Result<string>>
        {
            public string FullUrl { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.FullUrl).NotEmpty().WithMessage("ShortUrl cannot be empty.");
                RuleFor(x => x.FullUrl).Must(BeAValidShortUrl).WithMessage("Invalid ShortUrl format.");
            }

            private bool BeAValidShortUrl(string shortUrl)
            {
                return Uri.TryCreate(shortUrl, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
        }

        public class Handler : IRequestHandler<Query, Result<string>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var url = await _context.Urls.FirstOrDefaultAsync(x => x.ShortenedUrl == request.FullUrl);

                if (url == null)
                {
                    return Result<string>.Failure("ShortUrl not found.");
                }

                return Result<string>.Success(url.OriginalUrl);
            }
        }
    }
}