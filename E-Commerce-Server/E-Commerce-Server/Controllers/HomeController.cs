using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Server.Controllers
{
   
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper) 
        {
            _mapper = mapper;
        } 
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
