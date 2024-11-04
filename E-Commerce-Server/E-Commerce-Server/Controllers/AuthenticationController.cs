using ECom.API.DTO.AuthenticationDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECom.BLogic.Services;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using ECom.BLogic.Services.Models;
using System.Net;

using Microsoft.AspNetCore.Http.HttpResults;
using ECom.BLogic.Services.Authentication;
using ECom.API.DTOs.AuthenticationDTO;
using System.ComponentModel.DataAnnotations;
using Serilog;
//using Microsoft.AspNetCore.Components;

namespace ECom.API.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper, IAuthService authenticationService)
        {

            _authenticationService = authenticationService;
            _mapper = mapper;

        }

        [HttpGet("emailConfirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]EmailConfirmDTO confirmDTO)
        {

            Log.Information("Confirmation is reached!");
            EmailConfirmCredentials credentials = _mapper.Map<EmailConfirmCredentials>(confirmDTO);
            IdentityResult result = await _authenticationService.ConfirmEmailAsync(credentials);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                string errorMessage = result != null ? String.Join(' ', result.Errors.Select(x => x.Description).ToList()) : "Confirmation Failed";
                Log.Error(errorMessage);
                return Unauthorized(errorMessage);
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            UserCredentials user = _mapper.Map<UserCredentials>(request);
            var result = await _authenticationService.LoginAsync(user);
            if (result.Succeeded)
            {
                return Content(result.ToString());
            }
            return Unauthorized();
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
          //  if (ModelState.IsValid)
            //{
                UserCredentials user = _mapper.Map<UserCredentials>(request);
                var result = await _authenticationService.RegisterAsync(user);
                if (result.Succeeded)
                {
                    return Content(result.ToString());
                }
            string errorMessage = result != null ? String.Join(' ', result.Errors.Select(x => x.Description).ToList()) : "Registration Failed";
            Log.Error(errorMessage);
            return BadRequest(errorMessage);
            // }

        }
    }

}
