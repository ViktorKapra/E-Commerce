using ECom.BLogic.Services.DTOs;
using static ECom.Constants.DataEnums;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Platform>> GetTopPlatformsAsync(int count);
        Task<List<ProductDTO>> SearchAsync(ProductSearchDTO searchQuery);
    }
}
