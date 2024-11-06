﻿using ECom.Constants;
using System.ComponentModel.DataAnnotations;
namespace ECom.API.DTO.AuthenticationDTO
{
    public class LoginDTO
    {
        [Required]
        [StringLength(320, ErrorMessage = default)]
        [RegularExpression(ValidationConsts.EMAIL_REGEX,
            ErrorMessage = "Email address is not correct")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(ValidationConsts.PASSWORD_REGEX,
            ErrorMessage = " Your password must have at least 8 characters;\n must conatain at least: \n  1 uppercase character" +
            "\n 1 lowercase character;\n 1 number")]
        public string Password { get; set; }
    }
}
