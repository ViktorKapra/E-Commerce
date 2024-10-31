using ECom.BLogic.Services.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;



namespace ECom.BLogic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;



        public AuthService(SignInManager<IdentityUser> signInManager,
             UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(UserCredentials credentials)
        {

            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);            
            if (result.Succeeded)
            {   
                Log.Logger.Information("User logged in.");
            }
            return result;
        }
        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> Register(UserCredentials credentials)
        {
            var user = Activator.CreateInstance<IdentityUser>();


            user.UserName = credentials.Email;
            user.Email = credentials.Email;
            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                Log.Logger.Information("User created a new account with password.");

               /* var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailSender.SendEmailAsync(credentials.Email, "Confirm your email",
                    $"Please confirm your account by using this token {code}.");*/

            }
            return result;
        }
    }
}
    
