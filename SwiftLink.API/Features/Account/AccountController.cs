using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftLink.API.Core;

namespace SwiftLink.API.Features.Account
{
    public class AccountController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAccount.Request request)
        {
            LoginAccount.Query query = new(request);

            Result<LoginAccount.Response> result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                CookieOptions cookieOptions = new()
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SameSite = SameSiteMode.None,
                    Secure = true
                };

                Response.Cookies.Append("AuthToken", result.Value.Token, cookieOptions);
                result.Value.Token = string.Empty;

                return Ok(result.Value);
            };

            return BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccount.Request request)
        {
            // Validate the request using FluentValidation
            var validationResult = await new RegisterAccount.QueryValidator().ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                // Return BadRequest with validation errors
                return BadRequest(validationResult.Errors);
            }

            // Create a new query with the provided request
            var query = new RegisterAccount.Query
            {
                Data = request
            };

            // Send the query to the Mediator
            var result = await Mediator.Send(query);

            // Check if the registration was successful
            if (result.IsSuccess)
            {

                // Set the authentication cookie
                CookieOptions cookieOptions = new()
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SameSite = SameSiteMode.None,
                    Secure = true
                };

                Response.Cookies.Append("AuthToken", result.Value.Token, cookieOptions);

                // Clear sensitive data from the result
                result.Value.Token = string.Empty;

                // Return a Created response with user information
                return Created("registration successful", result.Value);
            }
            else
            {
                // Registration failed, return a BadRequest response with the error message
                return BadRequest(result.Error);
            }
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser()
        {
            UserAccount.Query query = new();

            Result<UserAccount.Response> result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Expire the existing authentication cookie
            Response.Cookies.Delete("AuthToken");

            // Optionally, perform any additional cleanup or logging out logic

            return Ok(new { message = "Logout successful" });
        }

    }
}