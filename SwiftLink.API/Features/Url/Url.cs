using SwiftLink.API.Features.Account;

namespace SwiftLink.API.Features.Url
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreationDate { get; set; }
        // public int Clicks { get; set; } not implemented yet
        // public bool isActive { get; set; } not implemented yet

        // Foreign key to AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}