using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Contracts;
using SwiftLink.API.Core;
using SwiftLink.API.Database;

namespace SwiftLink.API.Features.Account
{
    public static class RegisterAccount
    {
        public class Query : IRequest<Result<Response>>
        {
            public Request Data { get; set; }
        }

        public class Request
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
            public string Username { get; set; }
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
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}")
                    .WithMessage("Password must be between 4 and 8 characters and contain one uppercase letter, one lowercase letter, and one number. (e.g. Password1)");

                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Result<Response>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly DataContext _dbContext;
            private readonly ITokenService _tokenService;
            public Handler(UserManager<AppUser> userManager, DataContext dbContext, ITokenService tokenService)
            {
                _userManager = userManager;
                _dbContext = dbContext;
                _tokenService = tokenService;
            }

            public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Data.Email))
                    return Result<Response>.Failure("Email already exists.");
                if (await _userManager.Users.AnyAsync(x => x.UserName == request.Data.Username))
                    return Result<Response>.Failure("Username already exists.");

                var user = new AppUser
                {
                    DisplayName = request.Data.DisplayName,
                    Email = request.Data.Email,
                    UserName = request.Data.Username
                };

                var result = await _userManager.CreateAsync(user, request.Data.Password);

                if (result.Succeeded)
                {
                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    // Generate a token for the registered user
                    var token = _tokenService.CreateToken(user);

                    return Result<Response>.Success(new Response
                    {
                        Username = user.UserName!,
                        DisplayName = user.DisplayName!,
                        Email = user.Email!,
                        Token = token
                    });
                }
                else
                {
                    // Handle the case where user creation failed
                    return Result<Response>.Failure("User creation failed.");
                }
            }
        }

    }
}