using ECom.BLogic.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using Serilog;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using ECom.Data;
using ECom.BLogic.Services.EmailService;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;



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
        private string EncodeToken(string token) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        private string DecodeToken(string token) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

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
            string codeDecoded = DecodeToken(credentials.ConfirmationCode);
            IdentityResult result = await _userManager.ConfirmEmailAsync(user,codeDecoded);
            return result;
            
        }

        public async Task<SignInResult> LoginAsync(UserCredentials credentials)
        {

            //_signInManager.AuthenticationScheme
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
                code = EncodeToken(code);
                await _emailService.SendEmailAsync(credentials.Email, "Confirm your email",
                    $"Please confirm your account by using this token {code}."); 

            }
            return result;
        }

    }
}
    
