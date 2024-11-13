using AutoFixture;
using ECom.API.Exchanges.Profile;
using System.ComponentModel.DataAnnotations;

namespace ECom.Test.APITests.Exchanges
{
    public partial class ModelValidationTests
    {

        [Theory]
        [MemberData(nameof(wrongEmails))]
        public void UserExchange_Validation_IncorrectEmail_Format(string email)
        {
            //Arange
            var fixture = new Fixture();
            UserExchange request = new UserExchange()
            {
                Email = email
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();

            //Assert
            Assert.Contains("Email", failedMembers);
        }

        [Theory]
        [MemberData(nameof(correctEmails))]
        public void UserExchange_Validation_CorrectEmail_Format(string email)
        {
            //Arange
            var fixture = new Fixture();
            UserExchange request = new UserExchange()
            {
                Email = email
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();

            //Assert
            Assert.False(failedMembers.Contains("Email"));
        }

        [Theory]
        [MemberData(nameof(wrongNames))]
        public void UserExchange_Validation_Incorrect_LastName_Format(string lastName)
        {
            //Arange
            UserExchange request = new UserExchange()
            {
                LastName = lastName
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();

            //Assert
            Assert.Contains("LastName", failedMembers);
        }

        [Theory]
        [MemberData(nameof(correctNames))]
        public void UserExchange_Validation_Correct_FirstName_Format(string firstName)
        {
            //Arange
            UserExchange request = new UserExchange()
            {
                FirstName = firstName
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();

            //Assert
            Assert.False(failedMembers.Contains("FirstName"));
        }
        public static IEnumerable<object[]> correctNames =>
        new List<object[]>
        {
              new object[]{"Monica" },new object[] { "Anna-Maria" },
              new object[] { "Cin" },new object[] { "Cin-Cin" }
        };
        public static IEnumerable<object[]> wrongNames =>
        new List<object[]>
        {
              new object[]{"Monic21a" },new object[] { "Ann$a-Maria" },
              new object[] { "lkawni#" },new object[] { "$$$$" },new object[] { "." },
              new object[] { "Ole sla" },new object[] { "123l" }
        };
    }
}
