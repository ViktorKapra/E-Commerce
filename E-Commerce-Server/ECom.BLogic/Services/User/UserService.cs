using ECom.BLogic.Services.Interfaces;
using ECom.Data.Account;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Security.Claims;

namespace ECom.BLogic.Services.Profile
{
    public class UserService : IUserService
    {
        private readonly SignInManager<EComUser> _signInManager;
        private readonly UserManager<EComUser> _userManager;

        public UserService(SignInManager<EComUser> signInManager,
             UserManager<EComUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<EComUser> GetProfileInfoAsync(ClaimsPrincipal userClaims)
        {
            var result = await _userManager.GetUserAsync(userClaims);
            if (result == null)
            {
                Log.Error("User not found.");
                throw new Exception("User not found.");
            }
            return result;
        }
        public async Task<IdentityResult> UpdateProfileInfoAsync(EComUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> ChangePasswordAsync(EComUser user, string newPassword, string oldPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
    }
}
