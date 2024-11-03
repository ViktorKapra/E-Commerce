using System.ComponentModel.DataAnnotations;

namespace ECom.API.DTOs.AuthenticationDTO
{
    public class EmailConfirmDTO
    {
        [Required]
        [StringLength(320, ErrorMessage = "Email address is too long")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$",
            ErrorMessage = "Email addres is not correct")]
        public string Email { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}
