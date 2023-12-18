using SwiftLink.API.Features.Account;

namespace SwiftLink.API.Contracts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}