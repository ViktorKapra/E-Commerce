using System.ComponentModel.DataAnnotations;
using ECom.Constants;
namespace ECom.API.DTO.AuthenticationDTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression(Constants.ValidationConsts.EmailRegex,
          ErrorMessage = "Email address is not correct")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(Constants.ValidationConsts.PasswordRegex,
            ErrorMessage = " Your password must have at least 8 characters;\n must conatain at least: \n  1 uppercase character" +
            "\n 1 lowercase character;\n 1 number")]
        public string Password { get; set; }
    }
}
