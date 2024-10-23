using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet("/emailConfirm")]
        public IActionResult ConfirmEmail()
        {
            //TO DO 
            return StatusCode(501, "This feature is not implemented.");
        }
        [HttpPost("/sigIn")]
        public IActionResult Login()
        {
            // TO DO
            return StatusCode(501, "This feature is not implemented.");
        }
        [HttpPost("/signUp")]
        public IActionResult Register()
        {
            // TO DO
            return StatusCode(501, "This feature is not implemented.");
        }
    }
}
