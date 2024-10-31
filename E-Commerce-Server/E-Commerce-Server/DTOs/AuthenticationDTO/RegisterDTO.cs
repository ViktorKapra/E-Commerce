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
        [RegularExpression("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/",
            ErrorMessage = " Your password must be : have at 8 characters long; 1 uppercase & 1 lowercase character; 1 number")]
        public string Password { get; set; }
    }
}
