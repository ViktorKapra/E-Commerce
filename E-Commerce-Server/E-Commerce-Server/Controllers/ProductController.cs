using AutoMapper;
using ECom.API.Exchanges.Product;
using ECom.API.Filters;
using ECom.BLogic.DTOs;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, IUserService userService)
        {
            _productService = productService;
            _mapper = mapper;
            _userService = userService;
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
        /// <summary>
        /// Returns the found product based on the given id
        /// </summary>
        /// <remarks>Don't need authentication to be reached </remarks>
        /// <param name="id">The id of the the product</param>
        /// <response code="200"> List of products</response>
        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<ActionResult<ProductResponse>> GetProduct([FromQuery] int id)
        {
            var product = await _productService.GetProductAsync(id);
            return Ok(_mapper.Map<ProductResponse>(product));
        }

        /// <summary>
        /// Deletes the product based on the given id
        /// </summary>
        /// <remarks>Can be reached only by user with administration role </remarks>
        /// <param name="id">The id of the the product</param>
        /// <response code="204"></response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <remarks>Can be reached only by user with administration role </remarks>
        /// <param name="request"></param>
        /// /// <response code="201"></response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromForm] ProductRequest request)
        {
            var productDTO = _mapper.Map<ProductDTO>(request);
            var productImages = _mapper.Map<ProductImagesDTO>(request);
            await _productService.CreateProductAsync(productDTO, productImages);
            return Created();
        }

        /// <summary>
        /// Update existing product or create a new one
        /// </summary>
        /// <remarks>Can be reached only by user with administration role </remarks>
        /// <response code="200"></response>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromForm] int productID, [FromForm] ProductRequest request)
        {
            var productDTO = _mapper.Map<ProductDTO>(request);
            var productImages = _mapper.Map<ProductImagesDTO>(request);
            productDTO.Id = productID;
            await _productService.UpdateProductAsync(productDTO, productImages);
            return Ok();
        }

        /// <summary>
        /// Creates a new rating for the product
        /// </summary>
        /// <remarks> Needs authentication. Reflects on Total Rating of the product</remarks>
        /// <response code="200"> Returns the created rating</response>
        [Authorize]
        [HttpPost("rating")]
        public async Task<IActionResult> RateProduct([FromBody] ProductRatingExchange request)
        {
            var ratingDTO = _mapper.Map<ProductRatingDTO>(request);
            ratingDTO.UserClaim = HttpContext.User;
            var response = await _productService.RateProductAsync(ratingDTO);
            return Ok(_mapper.Map<ProductRatingExchange>(response));
        }

        ///<summary>
        /// Deletes the rating for the product
        ///</summary>
        /// /// <response code="204"></response>
        [Authorize]
        [HttpDelete("rating")]
        public async Task<IActionResult> DeleteRating([FromQuery] int productID)
        {
            var ratingDTO = new ProductRatingDTO { ProductId = productID, UserClaim = HttpContext.User };
            await _productService.DeleteRatingAsync(ratingDTO);
            return NoContent();
        }

        /// <summary>
        /// Returns the found products based on the filter query
        /// </summary>
        /// <remarks>Don't need authentication to be reached </remarks>
        /// <param name="request"></param>
        /// <response code="200"> List of products</response>
        [ServiceFilter(typeof(ValidationProductFilterAttribute))]
        [HttpGet("list")]
        public async Task<IActionResult> FilterProducts([FromQuery] ProductFilterRequest request)
        {
            var filterDTO = _mapper.Map<ProductFilterDTO>(request);
            List<ProductDTO> products = await _productService.FilterAsync(filterDTO);
            List<ProductResponse> productDTOs = products.Select(x => _mapper.Map<ProductResponse>(x)).ToList();
            return Ok(productDTOs);
        }
    }
}
