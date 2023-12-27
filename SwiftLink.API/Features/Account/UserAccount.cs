using MediatR;
using SwiftLink.API.Contracts;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Account
{
    public static class UserAccount
    {
        public record Query() : IRequest<Result<Response>>;

        public record Response
        {
            public string Username { get; set; } = default!;
            public string Email { get; set; } = default!;
        }

        internal sealed class Handler : IRequestHandler<Query, Result<Response>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(_userAccessor.GetUsername());

                if (user == null) return Result<Response>.Failure("User not found.");

                Response response = new()
                {
                    Username = user.UserName,
                    Email = user.Email
                };

                return Result<Response>.Success(response);
            }
        }
    }
}