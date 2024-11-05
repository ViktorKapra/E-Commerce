using System.ComponentModel.DataAnnotations;
using ECom.Constants;
namespace ECom.Configuration.Settings
{
    public class SmtpServerSettings
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression(ValidationConsts.EmailRegex)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(ValidationConsts.PasswordRegex)]
        public string Password { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Port { get; set; }
        public string SenderName { get; set; }
    }
}
