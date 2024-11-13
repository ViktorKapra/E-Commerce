using ECom.BLogic.Services.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<SignInResult> LoginAsync(UserCredentialsDTO credentials);
        public Task<IdentityResult> RegisterAsync(UserCredentialsDTO credentials);
        public Task<IdentityResult> ConfirmEmailAsync(EmailConfirmDTO credentials);
    }
}
