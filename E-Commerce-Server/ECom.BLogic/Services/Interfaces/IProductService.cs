using ECom.BLogic.Services.DTOs;
using Microsoft.AspNetCore.Http;
using static ECom.Constants.DataEnums;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Platform>> GetTopPlatformsAsync(int count);
        public Task<List<ProductDTO>> SearchAsync(ProductSearchDTO searchQuery);
        public Task<ProductDTO?> GetProductAsync(int id);
        public Task<bool> DeleteProductAsync(int id);
        public Task<bool> CreateProductAsync(ProductDTO productDTO, IFormFile? backgroundImage, IFormFile? logoImage);
        public Task<bool> UpdateProductAsync(ProductDTO productDTO, IFormFile? backgroundImage, IFormFile? logoImage);

    }
}
