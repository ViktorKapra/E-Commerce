using System.ComponentModel.DataAnnotations;

namespace ECom.API.DTO.AuthenticationDTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$",
          ErrorMessage = "Email addres is not correct")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = " Your password must have at least 8 characters;\n must conatain at least: \n  1 uppercase character" +
            "\n 1 lowercase character;\n 1 number")]
        public string Password { get; set; }
    }
}
