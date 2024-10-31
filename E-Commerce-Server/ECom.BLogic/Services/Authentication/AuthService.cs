﻿using Azure.Core;
using ECom.API.Areas.Identity.Pages.Account;
using ECom.BLogic.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
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
    
