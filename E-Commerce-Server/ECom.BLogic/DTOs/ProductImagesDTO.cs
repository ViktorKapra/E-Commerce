using Microsoft.AspNetCore.Http;

namespace ECom.BLogic.DTOs
{
    public class ProductImagesDTO
    {
        public IFormFile? Logo { get; set; }
        public IFormFile? Background { get; set; }
    }
}
