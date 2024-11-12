using ECom.BLogic.Templates;
using static ECom.Constants.DataEnums;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Platform>> GetTopPlatformsAsync(int count);
        Task<List<ECom.Data.Models.Product>> SearchAsync(SearchQuery<ECom.Data.Models.Product> searchQuery);
    }
}
