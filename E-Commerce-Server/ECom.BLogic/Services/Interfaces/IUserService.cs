using ECom.Data.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<EComUser> GetProfileInfoAsync(ClaimsPrincipal userClaims);
        public Task<IdentityResult> UpdateProfileInfoAsync(EComUser user);
        public Task<IdentityResult> ChangePasswordAsync(EComUser user, string newPassword, string oldPassword);
    }
}
