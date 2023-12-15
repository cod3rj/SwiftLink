using SwiftLink.API.Features.Account;

namespace SwiftLink.API.Features.Url
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        // public int Clicks { get; set; }
        // public bool isActive { get; set; }
    }
}