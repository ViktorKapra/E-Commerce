﻿using AutoFixture;
using ECom.API.DTO.AuthenticationDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECom.Test
{
    public partial class ModelValidationTests
    {
        [Fact]
        public void RegisterDTO_Validation_Null_Email_Password()
        {
            //Arrange
            string email = null;
            string password = null;
            RegisterDTO request = new RegisterDTO();
            var errors = new List<ValidationResult>();
            //Act 
            request.Email = email;
            request.Password = password;
            Validator.TryValidateObject(request, new ValidationContext(request), errors);
            //Assert
            Assert.Equal(2, errors.Count);
        }
        [Fact]
        public void RegisterDTO_Validation_Email_Format()
        {
            //Arange
            var fixture = new Fixture();
            RegisterDTO request = new RegisterDTO()
            { 
              Email = fixture.Create<string>(),
              Password = fixture.Create<string>()
            };
            var errors = new List<ValidationResult>();
            string emailFormat = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors,true);
            List<string> failedMembers = errors.SelectMany(x=>x.MemberNames).ToList();
             bool isCorrect = Regex.Match(request.Email, emailFormat).Success;
            //Assert
            if (isCorrect)
            {
                Assert.DoesNotContain("Email", failedMembers); 
            }
            else
            {
                Assert.Contains("Email", failedMembers);
            }
        }

        [Fact]
        public void RegisterDTOt_Validation_Short_Password()
        {
            //Arrange
            var fixture = new Fixture();
            RegisterDTO request = new RegisterDTO()
            {
                Email = fixture.Create<string>(),
                Password = fixture.Create<string>().Substring(0, 5)
            }; 
            var errors = new List<ValidationResult>();

            //Act 
            Validator.TryValidateObject(request, new ValidationContext(request), errors,true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.Contains("Password", failedMembers);
        }

        [Theory]
        [MemberData(nameof(weakPasswords))]
        public void RegisterDTO_Validation_Weak_Password(string password)
        {
            //Arrange
            var fixture = new Fixture();
            RegisterDTO request = new RegisterDTO()
            {
                Email = fixture.Create<string>(),
                Password = password
            };
            var errors = new List<ValidationResult>();

            //Act

            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.True(failedMembers.Contains("Password"));
        }

        [Theory]
        [MemberData(nameof(strongPasswords))]
        public void RegisterDTO_Validation_Strong_Password(string password)
        {
            //Arrange
            var fixture = new Fixture();
            RegisterDTO request = new RegisterDTO()
            {
                Email = fixture.Create<string>(),
                Password = password
            };
            var errors = new List<ValidationResult>();
            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.False(failedMembers.Contains("Password"));
        }
        
    }
}