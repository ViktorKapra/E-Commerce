using ECom.Constants;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Authentication
{
    public class EmailConfirmRequest
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
