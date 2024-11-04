using System.ComponentModel.DataAnnotations;

namespace ECom.Configuration.Settings
{
    public class SmtpServerSettings
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string Password { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Port { get; set; }
        public string SenderName { get; set; }
    }
}
