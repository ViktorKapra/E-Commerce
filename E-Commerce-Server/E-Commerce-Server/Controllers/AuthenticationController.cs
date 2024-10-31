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

namespace ECom.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthService _authenticationService;
        private readonly IMapper _mapper;

       
        public AuthenticationController( IMapper mapper, AuthService authenticationService)
        {

            _authenticationService = authenticationService;
            _mapper = mapper;
           
        }

        [HttpGet("/emailConfirm")]
        public IActionResult ConfirmEmail()
        {
            //TO DO 
            return StatusCode(501, "This feature is not implemented.");
        }
        [HttpPost("/sigIn")]

        public async Task<IActionResult> Login(LoginDTO request)
        {
            if (ModelState.IsValid)
            {
                UserCredentials user = _mapper.Map<UserCredentials>(request);
                var result = await _authenticationService.Login(user);
                if (result.Succeeded)
                {
                    return Content(result.ToString());
                }

                // TO DO
                
            }
            return StatusCode(401);
        }
            [HttpPost("/signUp")]
            [AllowAnonymous]
            public async Task<IActionResult> Register(RegisterDTO request)
            {
                UserCredentials user = _mapper.Map<UserCredentials>(request);
                var result = await _authenticationService.Register(user);
                if (result.Succeeded)
                {
                    return Content(result.ToString());
                }
            string errorMessage = result != null ? result.Errors.ToString() : "Registration failed";
                return StatusCode(400, errorMessage);
            }
    }
    
}
