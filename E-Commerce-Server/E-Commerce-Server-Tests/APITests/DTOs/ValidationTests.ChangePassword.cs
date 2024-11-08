using AutoFixture;
using ECom.API.DTOs.UserDTOs;
using System.ComponentModel.DataAnnotations;

namespace ECom.Test.APITests.DTOs
{
    public partial class ModelValidationTests
    {
        [Fact]
        public void ChangePasswordDTO_Validation_Null_Passwords()
        {
            //Arrange
            string newPassword = null;
            string oldPassword = null;
            ChangePasswordDTO request = new ChangePasswordDTO();
            var errors = new List<ValidationResult>();
            //Act
            request.NewPassword = newPassword;
            request.OldPassword = oldPassword;
            Validator.TryValidateObject(request, new ValidationContext(request), errors);
            //Assert
            Assert.Equal(2, errors.Count);
        }
        [Fact]
        public void ChangePasswordDTO_Validation_Short_NewPassword()
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordDTO request = new ChangePasswordDTO()
            {
                NewPassword = fixture.Create<string>().Substring(0, 5),
                OldPassword = fixture.Create<string>()
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.Contains("NewPassword", failedMembers);
        }

        [Theory]
        [MemberData(nameof(weakPasswords))]
        public void ChangePasswordDTO_Validation_Weak_NewPassword(string newPassword)
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordDTO request = new ChangePasswordDTO()
            {
                NewPassword = newPassword,
                OldPassword = fixture.Create<string>()
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            Assert.True(failedMembers.Contains("NewPassword"));
        }

        [Theory]
        [MemberData(nameof(strongPasswords))]
        public void ChangePasswordDTO_Validation_Strong_NewPassword(string newPassword)
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordDTO request = new ChangePasswordDTO()
            {
                NewPassword = newPassword,
                OldPassword = fixture.Create<string>()
            };
            var errors = new List<ValidationResult>();
            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.False(failedMembers.Contains("NewPassword"));
        }
    }
}
