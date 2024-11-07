using ECom.BLogic.Services.Interfaces;
using ECom.Data.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECom.BLogic.Services.Profile
{
    public class UserService : IUserService
    {
        private readonly SignInManager<EComUser> _signInManager;
        private readonly UserManager<EComUser> _userManager;
        private readonly IEmailService _emailService;

        public UserService(SignInManager<EComUser> signInManager,
             UserManager<EComUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<EComUser> GetProfileInfoAsync(ClaimsPrincipal userClaims)
        {
            return await _userManager.GetUserAsync(userClaims);
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
