using ECom.BLogic.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<SignInResult> LoginAsync(UserCredentials credentials);
        public Task<IdentityResult> RegisterAsync(UserCredentials credentials);
        public Task<IdentityResult> ConfirmEmailAsync(EmailConfirmCredentials credentials);
    }
}
