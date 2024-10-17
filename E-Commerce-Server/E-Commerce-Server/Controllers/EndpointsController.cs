using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Server.Controllers
{
   
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        [Route("/[controller]")]
        [Route("/[controller]/[action]")]
        public IActionResult GetInfo()
        {
            return Content("Hello world");
        }
    }
}
