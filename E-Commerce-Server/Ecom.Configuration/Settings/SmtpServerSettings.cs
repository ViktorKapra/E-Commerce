using ECom.Constants;
using System.ComponentModel.DataAnnotations;
namespace ECom.Configuration.Settings
{
    public class SmtpServerSettings
    {
        [Required]
        [StringLength(320, ErrorMessage = default)]
        [RegularExpression(ValidationConsts.EMAIL_REGEX)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(ValidationConsts.PASSWORD_REGEX)]
        public string Password { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Port { get; set; }
        public string SenderName { get; set; }
    }
}
