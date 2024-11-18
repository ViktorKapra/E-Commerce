using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECom.BLogic.Services.Image.Settings;
using ECom.BLogic.Services.Interfaces;
using ECom.Constants.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;

namespace ECom.BLogic.Services.Image
{
    public class ImageService : IImageService
    {
        private Cloudinary _cloudinary { get; set; }
        public ImageService(IOptions<CloudinarySettings> settings)
        {
            _cloudinary = new Cloudinary(settings.Value.GetCloudinaryUrl());
            _cloudinary.Api.Secure = settings.Value.IsSecure;
        }

        public async Task<string> UploadImageAsync(IFormFile image, string imageName, string folder = "")
        {
            var uploadResult = new ImageUploadResult();
            using (var imageStream = image.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, imageStream),
                    AssetFolder = folder,
                    PublicId = imageName
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            if (uploadResult.Error is not null)
            {
                throw new UploadImageException(uploadResult.Error.Message);
            }

            Log.Information("Image uploaded successfully");
            return uploadResult.SecureUrl.AbsoluteUri;

        }
        public async Task DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            if (result.Error is not null)
            {
                throw new DeleteImageException(result.Error.Message);
            }
            Log.Information("Image deleted successfully");
        }
    }
}

