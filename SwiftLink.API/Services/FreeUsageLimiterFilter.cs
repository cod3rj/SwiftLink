using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace SwiftLink.API.Services
{
    public class FreeUsageLimiterFilter : IAsyncActionFilter
    {
        private readonly IDistributedCache _cache;

        public FreeUsageLimiterFilter(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Define a unique key for the user or IP address
            string userKey = GetUserKeyFromContext(context);

            // Check the usage count from the cache
            string usageCountString = await _cache.GetStringAsync(userKey);
            int usageCount = string.IsNullOrEmpty(usageCountString) ? 0 : int.Parse(usageCountString);

            // Define the limit
            int limit = 10;

            // Check if the user has exceeded the limit
            if (usageCount >= limit)
            {
                context.Result = new BadRequestObjectResult("Free usage limit exceeded.");
                return;
            }

            // Increment the usage count
            usageCount++;
            await _cache.SetStringAsync(userKey, usageCount.ToString());

            // Continue with the action execution
            await next();
        }

        private string GetUserKeyFromContext(ActionExecutingContext context)
        {
            // Implement a logic to get a unique identifier for the user or IP address
            // For simplicity, let's assume the user's IP address is used as a key
            return context.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}