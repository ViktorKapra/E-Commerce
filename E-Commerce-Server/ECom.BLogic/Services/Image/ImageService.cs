using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECom.BLogic.Services.Image.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;

namespace ECom.BLogic.Services.Image
{
    public class ImageService
    {
        private Cloudinary _cloudinary { get; set; }
        public ImageService(IOptions<CloudinarySettings> settings)
        {
            _cloudinary = new Cloudinary(settings.Value.GetCloudinaryUrl());
            _cloudinary.Api.Secure = settings.Value.IsSecure;
        }

        public string GetImageURL(string publicId)
        {
            return _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
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
                Log.Error("Error uploading image: {error}", uploadResult.Error.Message);
                throw new Exception(uploadResult.Error.Message);
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
                Log.Error("Error deleting image: {error}", result.Error.Message);
                throw new Exception(result.Error.Message);
            }
            Log.Information("Image deleted successfully");
        }
    }
}

