using AutoFixture;
using ECom.API.Exchanges.User;
using System.ComponentModel.DataAnnotations;

namespace ECom.Test.APITests.Exchanges
{
    public partial class ModelValidationTests
    {
        [Fact]
        public void ChangePasswordRequest_Validation_Null_Passwords()
        {
            //Arrange
            string newPassword = null;
            string oldPassword = null;
            ChangePasswordRequest request = new ChangePasswordRequest();
            var errors = new List<ValidationResult>();
            //Act
            request.NewPassword = newPassword;
            request.OldPassword = oldPassword;
            Validator.TryValidateObject(request, new ValidationContext(request), errors);
            //Assert
            Assert.Equal(2, errors.Count);
        }
        [Fact]
        public void ChangePasswordRequest_Validation_Short_NewPassword()
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordRequest request = new ChangePasswordRequest()
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
        public void ChangePasswordRequest_Validation_Weak_NewPassword(string newPassword)
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordRequest request = new ChangePasswordRequest()
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
        public void ChangePasswordRequest_Validation_Strong_NewPassword(string newPassword)
        {
            //Arrange
            var fixture = new Fixture();
            ChangePasswordRequest request = new ChangePasswordRequest()
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
