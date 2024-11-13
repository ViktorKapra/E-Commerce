using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.User
{
    public class ChangePasswordRequest
    {
        [Required]
        [RegularExpression(Constants.ValidationConsts.PASSWORD_REGEX,
            ErrorMessage = " Your new password must have at least 8 characters;\n must conatain at least: \n  1 uppercase character" +
            "\n 1 lowercase character;\n 1 number")]
        public string NewPassword { get; set; }
        [Required]
        [RegularExpression(Constants.ValidationConsts.PASSWORD_REGEX)]
        public string OldPassword { get; set; }
    }
}
