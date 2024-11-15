﻿using AutoMapper;
using ECom.API.Exchanges.Product;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            if (id < 1)
            {
                var message = "Id must be greater than 0";
                Log.Error(message);
                return BadRequest();
            }
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
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
            if (id < 1)
            {
                var message = "Id must be greater than 0";
                Log.Error(message);
                return BadRequest();
            }
            bool deleteSecceded = await _productService.DeleteProductAsync(id);
            if (!deleteSecceded)
            {
                return NotFound();
            }
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
            var creationSucceded = await _productService.CreateProductAsync(productDTO, request.Background, request.Logo);
            if (creationSucceded)
            {
                return Created();
            }
            return BadRequest();
        }

        /// <summary>
        /// Update existing product or create a new one
        /// </summary>
        /// <remarks>Can be reached only by user with administration role </remarks>
        /// /// <response code="200"></response>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromForm] int productID, [FromForm] ProductRequest request)
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
