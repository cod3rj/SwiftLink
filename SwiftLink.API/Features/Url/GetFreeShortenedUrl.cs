using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Url
{
    public static class GetFreeShortenedUrl
    {
        public class Query : IRequest<Result<string>>
        {
            public int UrlId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.UrlId).NotEmpty().WithMessage("Id cannot be empty.");
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
                var url = await _context.Urls.FirstOrDefaultAsync(x => x.Id == request.UrlId);

                if (url == null)
                {
                    return Result<string>.Failure("Url not found.");
                }

                return Result<string>.Success(url.ShortenedUrl);
            }
        }
    }
}