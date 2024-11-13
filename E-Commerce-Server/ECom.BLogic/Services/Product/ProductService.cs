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
