using AutoMapper;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Templates;
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
        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            // To manage images, if we need to delete them
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public Task<bool> CreateProductAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
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
    }
}
