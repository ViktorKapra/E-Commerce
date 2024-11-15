using ECom.BLogic.DTOs;
using ECom.BLogic.Services.DTOs;
using static ECom.Constants.DataEnums;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Platform>> GetTopPlatformsAsync(int count);
        public Task<List<ProductDTO>> SearchAsync(ProductSearchDTO searchQuery);
        public Task<ProductDTO?> GetProductAsync(int id);
        public Task<bool> DeleteProductAsync(int id);
        public Task<bool> CreateProductAsync(ProductDTO productDTO, ProductImagesDTO prouctImagesDTO);
        public Task<bool> UpdateProductAsync(ProductDTO productDTO, ProductImagesDTO prouctImagesDTO);

    }
}
