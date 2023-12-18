using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SwiftLink.API.Contracts;
using SwiftLink.API.Core;

namespace SwiftLink.API.Features.Account
{
    public static class LoginAccount
    {
        public record Query(Request data) : IRequest<Result<Response>>;

        public class Request
        {
            public string Email { get; init; }
            public string Password { get; init; }
        }

        public class Response
        {
            public string Username { get; init; }
            public string DisplayName { get; init; }
            public string Email { get; set; }
            public string Token { get; set; } = default!;
        }
        public class QueryValidator : AbstractValidator<Request>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            }
        }

        public class Handler : IRequestHandler<Query, Result<Response>>
        {
            private readonly ITokenService _tokenService;
            private readonly UserManager<AppUser> _userManager;

            public Handler(ITokenService tokenService, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _tokenService = tokenService;
            }

            public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.data.Email);

                if (user == null)
                    return Result<Response>.Failure("Invalid email or password.");

                var result = await _userManager.CheckPasswordAsync(user, request.data.Password);

                if (!result)
                {
                    return Result<Response>.Failure("Invalid email or password.");
                }

                return Result<Response>.Success(new Response
                {
                    Username = user.UserName!,
                    DisplayName = user.DisplayName!,
                    Email = user.Email!,
                    Token = _tokenService.CreateToken(user)
                });
            }
        }
    }
}