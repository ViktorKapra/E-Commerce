using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ECom.BLogic.Services.Image.Settings
{
    public class CloudinarySettings
    {
        [Required, NotNull]
        public string ApiSecret { get; set; }
        [Required, NotNull]
        public string ApiKey { get; set; }
        [Required, NotNull]
        public string CloudName { get; set; }
        public bool IsSecure { get; set; } = true;
        public string GetCloudinaryUrl()
        {
            return $"cloudinary://{ApiKey}:{ApiSecret}@{CloudName}";
        }
    }
}
