using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace ECom.API.Controllers
{

    [ApiController]
    [Route("/api/home")]
    [Authorize(Roles = "Admin")]
    public class HomeController : ControllerBase
    {
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper)
        {
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetInfo()
        {

            IPAddress? client = HttpContext.Connection.RemoteIpAddress;
            string clientString = client == null ? "unknown" : client.ToString();
            Log.Information("Method GetInfo was approached by " + clientString);
            return Content("Hello world");
        }
    }
}
