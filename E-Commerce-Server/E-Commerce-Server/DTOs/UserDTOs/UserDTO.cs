using ECom.Constants;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.DTOs.ProfileDTOs
{
    public class UserDTO
    {
        [Required]
        [StringLength(320)]
        [RegularExpression(ValidationConsts.EMAIL_REGEX,
            ErrorMessage = "Email address is not valid!")]
        public string Email { get; set; }


        [RegularExpression(ValidationConsts.NAME_REGEX,
        ErrorMessage = "Only letters are allowed.")]
        public string? FirstName { get; set; }

        [RegularExpression(ValidationConsts.NAME_REGEX,
            ErrorMessage = "Only letters are allowed.")]
        public string? LastName { get; set; }

        [RegularExpression(ValidationConsts.PHONE_REGEX,
          ErrorMessage = "Phone number is not correct." +
            " Phone number must have the following form ***-***-**** where '*' stands for digit.")]
        public string? PhoneNumber { get; set; }
        public string? AddressDelivery { get; set; }
    }
}
