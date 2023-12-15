using SwiftLink.API.Database;
using SwiftLink.API.Features.Url;

namespace SwiftLink.API.Services
{
    public class UrlService
    {
        private readonly DataContext _context;

        public UrlService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> CreateShortUrl(string originalUrl)
        {
            // Validate originalUrl
            if (!IsValidUrl(originalUrl))
            {
                throw new ArgumentException("Invalid original URL.");
            }

            string uniqueIdentifier = GenerateUniqueIdentifier();
            string shortUrl = $"http://localhost:5000/{uniqueIdentifier}";

            Url url = new Url
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortUrl,
                CreationDate = DateTime.UtcNow
            };

            _context.Urls.Add(url);

            await _context.SaveChangesAsync();

            return shortUrl;
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        private string GenerateUniqueIdentifier()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}