using AutoMapper;
using ECom.API.DTOs.ProfileDTOs;
using ECom.API.DTOs.UserDTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.Data.Account;
using ECom.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text;

namespace ECom.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;

        }

        /// <summary>
        /// Get the profile information of the authenticated user.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves the profile information of the authenticated user.
        /// </remarks>
        /// <returns>The profile information of the authenticated user.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> GetProfileInfo()
        {
            EComUser user = await _userService.GetProfileInfoAsync(HttpContext.User);
            UserDTO userResponce = _mapper.Map<UserDTO>(user);
            return Ok(userResponce);
        }

        /// <summary>
        /// Update the profile information of the authenticated user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the authenticated user to update their profile information.
        /// </remarks>
        /// <param name="profileInfo">The updated profile information.</param>
        /// <response code = "200" >Profile data updated successfully  </response>
        /// <response code = "400" >Update user failed. Can be followed by addtitional errors.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateProfileInfo(UserDTO profileInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            EComUser user = await _userService.GetProfileInfoAsync(HttpContext.User);
            _mapper.Map(profileInfo, user);
            var result = await _userService.UpdateProfileInfoAsync(user);

            if (!result.Succeeded)
            {
                string errorMessage = result.GetErrorsDescriptions("Updating Failed");
                Log.Error(errorMessage);
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        /// <summary>
        /// Change password of the authenticated user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the authenticated user to update their password. It requires the old password to be provided.
        /// </remarks>
        /// <param name="passwordPatch">Json patch, consisting the new and old password</param>
        /// <response code = "200" >Password updated successfully  </response>
        /// <response code = "400" >Password change failed. Can be followed by additional errors</response>
        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword([FromBody] JsonPatchDocument<ChangePasswordDTO> passwordPatch)
        {
            ChangePasswordDTO paswords = new ChangePasswordDTO();
            passwordPatch.ApplyTo(paswords);
            StringBuilder errorMessage = new StringBuilder();
            EComUser user = await _userService.GetProfileInfoAsync(HttpContext.User);
            var result = await _userService.ChangePasswordAsync(user, paswords.NewPassword, paswords.OldPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            errorMessage.Append(result.GetErrorsDescriptions("Password change failed"));
            Log.Error(errorMessage.ToString());
            return BadRequest(errorMessage.ToString());
        }
    }
}
