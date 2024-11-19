using AutoMapper;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.Data.Account;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Runtime.CompilerServices;
using System.Security.Claims;

[assembly: InternalsVisibleTo("ECom.Test")]

namespace ECom.BLogic.Services.Profile
{
    public class UserService : IUserService
    {
        private readonly SignInManager<EComUser> _signInManager;
        private readonly UserManager<EComUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(SignInManager<EComUser> signInManager,
             UserManager<EComUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        async Task<EComUser> IUserService.GetUserAsync(ClaimsPrincipal userClaims)
        {
            var result = await _userManager.GetUserAsync(userClaims);
            if (result == null)
            {
                Log.Error("User not found.");
                throw new Exception("User not found.");
            }
            return result;
        }

        public async Task<UserDTO> GetProfileInfoAsync(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<IdentityResult> UpdateProfileInfoAsync(UserDTO userDTO, ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            _mapper.Map(userDTO, user);
            return await _userManager.UpdateAsync(user!);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal userClaims, string oldPassword, string newPassword)
        {
            EComUser user = await _userManager.GetUserAsync(userClaims);
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

    }
}
