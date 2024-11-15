using AutoMapper;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Templates;
using ECom.Constants;
using ECom.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static ECom.Constants.DataEnums;

namespace ECom.BLogic.Services.Product
{
    public class ProductService : IProductService
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        public readonly IImageService _imageService;
        public ProductService(ApplicationDbContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }
        private async Task UploadImagesForProduct(ECom.Data.Models.Product product,
            IFormFile? backgroundImage, IFormFile? logoImage)
        {
            try
            {
                product.Background = backgroundImage is null ? null :
                                   await _imageService.UploadImageAsync(backgroundImage, $"background_{product.Id}", PathsConsts.PRODUCT_BACKGORUND_IMAGE_PATH);
                product.Logo = logoImage is null ? null :
                               await _imageService.UploadImageAsync(logoImage, $"logo_{product.Id}", PathsConsts.PRODUCT_LOGO_IMAGE_PATH);
            }
            catch (Exception e) { Log.Error(e, "Error uploading images"); }
        }
        private async Task DeleteImagesForProduct(ECom.Data.Models.Product product)
        {
            try
            {
                if (product.Background is not null)
                {
                    await _imageService.DeleteImageAsync(product.Background);
                }
                if (product.Logo is not null)
                {
                    await _imageService.DeleteImageAsync(product.Logo);
                }
            }
            catch (Exception e) { Log.Error(e, "Error deleting images"); }
        }

        private async Task<ECom.Data.Models.Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<ProductDTO?> GetProductAsync(int id)
        {
            var product = await GetProductById(id);
            var productDTO = product is not null ? _mapper.Map<ProductDTO>(product) : null;
            return productDTO;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductById(id);
            if (product == null)
            {
                Log.Error("Product with id {id} not found", id);
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateProductAsync(ProductDTO productDTO, IFormFile? backgroundImage, IFormFile? logoImage)
        {
            var product = _mapper.Map<ECom.Data.Models.Product>(productDTO);

            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating product");
                return false;
            }

            await UploadImagesForProduct(product, backgroundImage, logoImage);
            await _context.SaveChangesAsync();
            Log.Information("Product with id {id} created", product.Id);
            return true;
        }
        public async Task<bool> UpdateProductAsync(ProductDTO productDTO, IFormFile? backgroundImage, IFormFile? logoImage)
        {
            var product = await GetProductById(productDTO.Id);
            if (product is null)
            {
                return await CreateProductAsync(productDTO, backgroundImage, logoImage);
            }

            try
            {
                _mapper.Map(productDTO, product);
                _context.Update(product);
                await DeleteImagesForProduct(product);
                await UploadImagesForProduct(product, backgroundImage, logoImage);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error updating product");
                return false;
            }
            Log.Information("Product with id {id} updated", product.Id);
            return true;
        }
        public async Task<List<ProductDTO>> SearchAsync(ProductSearchDTO searchDTO)
        {
            var query = _mapper.Map<SearchQuery<ECom.Data.Models.Product>>(searchDTO);

            var entities = await _context.Products
                .Where(query.Expression)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();
            var productDTOs = entities.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            return productDTOs;
        }

        public async Task<List<Platform>> GetTopPlatformsAsync(int count)
        {
            if (count < 1)
            {
                var message = "Count must be greater than 0";
                Log.Error(message);
                throw new ArgumentException(message);
            }
            return await _context.Products
                        .GroupBy(p => p.Platform)
                        .OrderByDescending(g => g.Count())
                        .Take(count)
                        .Select(g => g.Key)
                        .ToListAsync();
        }

    }
}
