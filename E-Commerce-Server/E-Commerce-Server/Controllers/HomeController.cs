using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using System.Reflection;
using System.Text;


namespace ECom.API.Controllers
{

    [ApiController]
    [Route("/api/home")]
    [Authorize]
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
