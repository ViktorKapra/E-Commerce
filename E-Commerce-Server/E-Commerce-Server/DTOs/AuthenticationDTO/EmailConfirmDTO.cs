using ECom.Constants;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.DTOs.AuthenticationDTO
{
    public class EmailConfirmDTO
    {
        [Required]
        [StringLength(320)]
        [RegularExpression(ValidationConsts.EMAIL_REGEX,
          ErrorMessage = "Email addres is not correct")]
        public string Email { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}
