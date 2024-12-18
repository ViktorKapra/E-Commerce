﻿using AutoMapper;
using ECom.API.Exchanges.Authentication;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmRequest confirmRequest)
        {
            Log.Information("Confirmation is reached!");
            EmailConfirmDTO credentials = _mapper.Map<EmailConfirmDTO>(confirmRequest);
            IdentityResult result = await _authenticationService.ConfirmEmailAsync(credentials);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                string errorMessage = result.GetErrorsDescriptions("Confirmation Failed!");
                Log.Error(errorMessage);
                return Unauthorized(errorMessage);
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            UserCredentialsDTO user = _mapper.Map<UserCredentialsDTO>(request);
            var result = await _authenticationService.LoginAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            UserCredentialsDTO user = _mapper.Map<UserCredentialsDTO>(request);
            var result = await _authenticationService.RegisterAsync(user);
            if (result.Succeeded)
            {
                return Created();
            }
            string errorMessage = result.GetErrorsDescriptions("Registration Failed");
            Log.Error(errorMessage);
            return BadRequest(errorMessage);
        }
    }
}
