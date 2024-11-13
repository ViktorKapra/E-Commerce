using ECom.BLogic.Services.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetProfileInfoAsync(ClaimsPrincipal userClaims);
        public Task<IdentityResult> UpdateProfileInfoAsync(UserDTO userDTO, ClaimsPrincipal userClaims);
        public Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal userClaims, string oldPassword, string newPassword);
    }
}
