using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftLink.API.Core;
using SwiftLink.API.Services;

namespace SwiftLink.API.Features.Url
{
    public class UrlController : BaseRouteController
    {

        // Will place a limiter here for the free version.
        [HttpGet("free/{urlId}")]
        public async Task<IActionResult> GetFreeShortenedUrlById(int urlId)
        {
            return HandleResult(await Mediator.Send(new GetFreeShortenedUrl.Query { UrlId = urlId }));
        }

        [HttpGet("u/{urlId}")]
        public async Task<IActionResult> GetShortenedUrlById(int urlId)
        {
            return HandleResult(await Mediator.Send(new GetShortenedUrl.Query { UrlId = urlId }));
        }

        [HttpGet("uAll")]
        public async Task<IActionResult> GetShortenedUrls()
        {
            return HandleResult(await Mediator.Send(new GetShortenedUrlsList.Query()));
        }

        [HttpDelete("d/{urlId}")]
        public async Task<IActionResult> DeleteUrl(int urlId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { UrlId = urlId }));
        }

        [HttpGet("{shortUrl}")]
        [AllowAnonymous]
        public async Task<IActionResult> RedirectShortUrl(string shortUrl)
        {
            // Construct the full URL using the base URL of your application
            var fullUrl = $"http://localhost:5000/{shortUrl}";

            // Retrieve the original URL from the database
            var result = await Mediator.Send(new GetRedirectUrl.Query { FullUrl = fullUrl });

            // Check if the result was successful
            if (result.IsSuccess)
            {
                // Redirect to the original URL
                return Redirect(result.Value);
            }
            else
            {
                // Handle the failure, for example, return a 404 Not Found
                return NotFound(result.Error);
            }
        }

        [HttpPost("url")]
        public async Task<IActionResult> Create([FromBody] Create.Request request)
        {
            Create.Command command = new(request);

            return HandleResult(await Mediator.Send(command));
        }

        [HttpPost("free/url")]
        [AllowAnonymous]
        [ServiceFilter(typeof(FreeUsageLimiterFilter))] // Apply the limiter only to this action
        public async Task<IActionResult> CreateFree([FromBody] Create.Request request)
        {
            Create.Command command = new(request);

            return HandleResult(await Mediator.Send(command));
        }
    }
}