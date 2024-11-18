using AutoMapper;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Templates;
using ECom.Constants;
using ECom.Constants.Exceptions;
using ECom.Data;
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
                                                  ProductImagesDTO prouctImagesDTO)
        {

            product.Background = prouctImagesDTO.Background is null
                               ? null
                               : await _imageService.UploadImageAsync(prouctImagesDTO.Background,
                                                                      $"background_{product.Id}",
                                                                      PathsConsts.PRODUCT_BACKGORUND_IMAGE_PATH);
            product.Logo = prouctImagesDTO.Logo is null ? null :
                           await _imageService.UploadImageAsync(prouctImagesDTO.Logo,
                                                                $"logo_{product.Id}",
                                                                PathsConsts.PRODUCT_LOGO_IMAGE_PATH);

        }
        private async Task DeleteImagesForProduct(ECom.Data.Models.Product product)
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

        private async Task<ECom.Data.Models.Product> GetProductById(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                var message = $"Product with id {id} is not found";
                Log.Error(message);
                throw new NonExistnantException(message);
            }
            return result;
        }

        public async Task<ProductDTO> GetProductAsync(int id)
        {
            var product = await GetProductById(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductById(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task CreateProductAsync(ProductDTO productDTO, ProductImagesDTO productImagesDTO)
        {
            var product = _mapper.Map<ECom.Data.Models.Product>(productDTO);

            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidCreationException(e.Message);
            }

            await UploadImagesForProduct(product, productImagesDTO);
            await _context.SaveChangesAsync();
            Log.Information("Product with id {id} created", product.Id);
        }

        public async Task UpdateProductAsync(ProductDTO productDTO, ProductImagesDTO prouctImagesDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productDTO.Id);
            if (product is null)
            {
                await CreateProductAsync(productDTO, prouctImagesDTO);
                return;
            }

            try
            {
                _mapper.Map<ProductDTO, ECom.Data.Models.Product>(productDTO, product);
                _context.Update(product);
            }
            catch (Exception e)
            {
                throw new ProductUpdateFailedException(e.Message);
            }
            await DeleteImagesForProduct(product);
            await UploadImagesForProduct(product, prouctImagesDTO);
            await _context.SaveChangesAsync();
            Log.Information("Product with id {id} updated", product.Id);
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
                throw new InvalidArgumentException(message);
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
