using System.Data;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Url
{
    public static class GetShortenedUrl
    {
        public class Query : IRequest<Result<UrlDto>>
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

        public class Handler : IRequestHandler<Query, Result<UrlDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<UrlDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var url = await _context.Urls.FirstOrDefaultAsync(x => x.Id == request.UrlId);

                if (url == null)
                {
                    return Result<UrlDto>.Failure("Url not found.");
                }

                var urlDto = _mapper.Map<UrlDto>(url);

                return Result<UrlDto>.Success(urlDto);
            }
        }
    }
}