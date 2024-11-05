using System.ComponentModel.DataAnnotations;
using ECom.Constants;

namespace ECom.API.DTOs.AuthenticationDTO
{
    public class EmailConfirmDTO
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression(ValidationConsts.EmailRegex,
          ErrorMessage = "Email addres is not correct")]
        public string Email { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}
