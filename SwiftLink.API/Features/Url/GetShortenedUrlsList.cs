using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Url
{
    public static class GetShortenedUrlsList
    {
        public class Query : IRequest<Result<List<UrlDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<UrlDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<UrlDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var urls = await _context.Urls.OrderBy(x => x.CreationDate).ToListAsync();

                    if (urls == null)
                    {
                        return Result<List<UrlDto>>.Failure("No Urls found.");
                    }

                    var urlDtos = _mapper.Map<List<UrlDto>>(urls);

                    return Result<List<UrlDto>>.Success(urlDtos);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    return Result<List<UrlDto>>.Failure($"Failed to retrieve URLs. {ex.Message}");
                }
            }
        }
    }
}