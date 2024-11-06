using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Models;
using ECom.Data.Account;
using ECom.Extensions;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace ECom.BLogic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<EComUser> _signInManager;
        private readonly UserManager<EComUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthService(SignInManager<EComUser> signInManager,
             UserManager<EComUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(EmailConfirmCredentials credentials)
        {
            EComUser? user = await _userManager.FindByEmailAsync(credentials.Email);

            if (user == null)
            {
                IdentityError notFound = new IdentityError();
                notFound.Description = $"User with email {credentials.Email} was not found!";
                Log.Error(notFound.Description);
                return IdentityResult.Failed(notFound);
            }
            string codeDecoded = credentials.ConfirmationCode.DecodeToken();
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, codeDecoded);
            return result;
        }

        public async Task<SignInResult> LoginAsync(UserCredentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Log.Logger.Information("User logged in.");
            }
            return result;
        }

        public async Task<IdentityResult> RegisterAsync(UserCredentials credentials)
        {
            var user = Activator.CreateInstance<EComUser>();
            user.UserName = credentials.Email;
            user.Email = credentials.Email;
            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                Log.Logger.Information("User created a new account with password.");
                var createdUser = await _userManager.FindByEmailAsync(user.Email);
                await _userManager.AddToRoleAsync(createdUser, "User");

                string code = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
                code = code.EncodeToken();
                await _emailService.SendEmailAsync(credentials.Email, "Confirm your email",
                    $"Please confirm your account by using this token {code}.");
            }
            return result;
        }
    }
}

