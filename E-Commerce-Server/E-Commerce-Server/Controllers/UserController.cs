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

        [HttpGet]
        public async Task<IActionResult> GetProfileInfo()
        {
            EComUser user = await _userService.GetProfileInfoAsync(HttpContext.User);
            UserDTO userResponce = _mapper.Map<UserDTO>(user);
            return Ok(userResponce);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfileInfo(UserDTO profileInfo)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            EComUser user = await _userService.GetProfileInfoAsync(HttpContext.User);
            _mapper.Map(profileInfo, user);
            var result = await _userService.UpdateProfileInfoAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                string errorMessage = result.GetErrorsDescriptions("Updating Failed");
                Log.Error(errorMessage);
                return BadRequest(errorMessage);
            }
        }
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
            else
            {
                errorMessage.Append(result.GetErrorsDescriptions("Password change failed"));
            }
            Log.Error(errorMessage.ToString());
            return BadRequest(errorMessage.ToString());
        }
    }
}
