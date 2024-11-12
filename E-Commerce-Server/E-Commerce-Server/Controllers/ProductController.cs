using AutoMapper;
using ECom.API.DTOs.ProductDTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Templates;
using ECom.Data.Models;
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
        public async Task<ActionResult<List<ProductDTO>>> SearchProducts([FromQuery] ProductsSearchDTO request)
        {
            var searchQuery = _mapper.Map<SearchQuery<Product>>(request);
            List<Product> products = await productService.SearchAsync(searchQuery);
            List<ProductDTO> productDTOs = products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            return Ok(productDTOs);
        }
    }
}
