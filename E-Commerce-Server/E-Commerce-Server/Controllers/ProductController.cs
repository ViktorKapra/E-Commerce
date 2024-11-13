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
    [AllowAnonymous]
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
        [HttpGet("search")]
        public async Task<ActionResult<List<ProductResponse>>> SearchProducts([FromQuery] ProductsSearchRequest request)
        {
            var searchDTO = _mapper.Map<ProductSearchDTO>(request);
            List<ProductDTO> products = await productService.SearchAsync(searchDTO);
            List<ProductResponse> productDTOs = products.Select(x => _mapper.Map<ProductResponse>(x)).ToList();
            return Ok(productDTOs);
        }
    }
}
