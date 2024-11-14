using AutoMapper;
using ECom.API.Exchanges.Product;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/games")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the top 3 platforms with the most games
        /// </summary>
        /// <remarks>Don't need authentication to be reached </remarks>
        /// <response code="200"> List of platforms</response>
        [AllowAnonymous]
        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var platformCount = 3;
            var platforms = await productService.GetTopPlatformsAsync(platformCount);
            return Ok(platforms.Select(x => x.ToString()).ToList());
        }

        /// <summary>
        /// Returns the found products based on the search query
        /// </summary>
        /// <remarks>Don't need authentication to be reached </remarks>
        /// <param name="request">The updated profile information.</param>
        /// <response code="200"> List of products</response>
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<List<ProductExchange>>> SearchProducts([FromQuery] ProductsSearchRequest request)
        {
            var searchDTO = _mapper.Map<ProductSearchDTO>(request);
            List<ProductDTO> products = await productService.SearchAsync(searchDTO);
            List<ProductExchange> productDTOs = products.Select(x => _mapper.Map<ProductExchange>(x)).ToList();
            return Ok(productDTOs);
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<ActionResult<ProductExchange>> GetProduct([FromQuery] int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductExchange>(product));
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            bool deleteSecceded = await productService.DeleteProductAsync(id);
            if (!deleteSecceded)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] ProductExchange request)
        {
            var productDTO = _mapper.Map<ProductDTO>(request);
            var creationSucceded = await productService.CreateProductAsync(productDTO);
            if (creationSucceded)
            {
                return Created();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] ProductExchange request)
        {
            return Ok("This is not implemented");
        }
    }
}
