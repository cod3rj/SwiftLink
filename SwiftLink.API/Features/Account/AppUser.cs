using Microsoft.AspNetCore.Identity;

namespace SwiftLink.API.Features.Account
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public ICollection<Url.Url> Urls { get; set; } = new List<Url.Url>();
    }
}