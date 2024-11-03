using ECom.BLogic.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLogic.Services.Authentication
{
    public interface IAuthService
    {
        public Task<SignInResult> LoginAsync(UserCredentials credentials);
        public Task<IdentityResult> RegisterAsync(UserCredentials credentials);
        public Task<IdentityResult> ConfirmEmailAsync(EmailConfirmCredentials credentials);
    }
}
