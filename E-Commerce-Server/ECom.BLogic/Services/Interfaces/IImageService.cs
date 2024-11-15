using Microsoft.AspNetCore.Http;

namespace ECom.BLogic.Services.Interfaces
{
    public interface IImageService
    {
        public Task<string> UploadImageAsync(IFormFile image, string imageName, string folder);
        public Task DeleteImageAsync(string publicId);
    }
}
