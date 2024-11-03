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



namespace ECom.BLogic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<EComUser> _signInManager;
        private readonly UserManager<EComUser> _userManager;
        


        public AuthService(SignInManager<EComUser> signInManager,
             UserManager<EComUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(EmailConfirmCredentials credentials)
        {
            
            EComUser? user = await _userManager.FindByEmailAsync(credentials.Email);

            if (user == null) 
            {
                IdentityError notFound = new IdentityError();
                notFound.Description = $"User with email {credentials.Email} was not found!";
                return IdentityResult.Failed(notFound);
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, credentials.ConfirmationCode);
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

                string userId = await _userManager.GetUserIdAsync(user);
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //await SendConfirmationEmailAsync(user.Email, code);

                /*await _emailSender.SendEmailAsync(credentials.Email, "Confirm your email",
                    $"Please confirm your account by using this token {code}."); */

            }
            return result;
        }
        /*private async Task SendConfirmationEmailAsync(string email, string code)
        {
            MailMessage mailMessage = new MailMessage("ECommerce@mail.com",email);
            mailMessage.Subject = "Confirm your email";
            mailMessage.Body = $"Please confirm your account by using this token {code}.";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.maileroo.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("SenderEmail," "SenderPassword");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email Sent Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }*/

    }
}
    
