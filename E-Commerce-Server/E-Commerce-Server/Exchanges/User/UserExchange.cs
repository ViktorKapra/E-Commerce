using ECom.Constants;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Profile
{
    public class UserExchange
    {
        /// <summary>
        /// The email address of the user.
        /// </summary>
        /// <example>user@example.com</example>
        [Required]
        [StringLength(320)]
        [RegularExpression(ValidationConsts.EMAIL_REGEX,
            ErrorMessage = "Email address is not valid!")]
        public string Email { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        /// <example>John</example>
        [RegularExpression(ValidationConsts.NAME_REGEX,
        ErrorMessage = "Only letters are allowed.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        /// <example>Doe</example>
        [RegularExpression(ValidationConsts.NAME_REGEX,
            ErrorMessage = "Only letters are allowed.")]
        public string? LastName { get; set; }

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        /// <example>123-456-7890</example>
        [RegularExpression(ValidationConsts.PHONE_REGEX,
          ErrorMessage = "Phone number is not correct." +
            " Phone number must have the following form ***-***-**** where '*' stands for digit.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The delivery address of the user.
        /// </summary>
        /// <example>123 Main St</example>
        public string? AddressDelivery { get; set; }
    }
}
