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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
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
            var platforms = await _productService.GetTopPlatformsAsync(platformCount);
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
        public async Task<ActionResult<List<ProductResponse>>> SearchProducts([FromQuery] ProductsSearchRequest request)
        {
            var searchDTO = _mapper.Map<ProductSearchDTO>(request);
            List<ProductDTO> products = await _productService.SearchAsync(searchDTO);
            List<ProductResponse> productDTOs = products.Select(x => _mapper.Map<ProductResponse>(x)).ToList();
            return Ok(productDTOs);
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<ActionResult<ProductResponse>> GetProduct([FromQuery] int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductResponse>(product));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            bool deleteSecceded = await _productService.DeleteProductAsync(id);
            if (!deleteSecceded)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromForm] ProductRequest request)
        {
            var productDTO = _mapper.Map<ProductDTO>(request);
            var creationSucceded = await _productService.CreateProductAsync(productDTO, request.Background, request.Logo);
            if (creationSucceded)
            {
                return Created();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromForm] int productID, ProductRequest request)
        {
            var requestSucceded = false;
            var productDTO = _mapper.Map<ProductDTO>(request);
            productDTO.Id = productID;
            requestSucceded = await _productService.UpdateProductAsync(productDTO, request.Background, request.Logo);
            if (!requestSucceded)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
