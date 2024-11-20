using AutoFixture;
using ECom.BLogic.Services.Authentication;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.Data.Account;
using ECom.Extensions;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;

namespace ECom.Test.BLogicTests
{
    public class AuthServiceTests
    {
        private UserManager<EComUser> _userManager;
        private SignInManager<EComUser> _signInManager;
        private AuthService _authService;
        private IEmailService _emailService;

        public AuthServiceTests()
        {
            _userManager = A.Fake<UserManager<EComUser>>();
            _signInManager = A.Fake<SignInManager<EComUser>>();
            _emailService = A.Fake<IEmailService>();
            _authService = new AuthService(_signInManager, _userManager, _emailService);
        }
        public static IEnumerable<object[]> userCredentials =>
            new List<object[]> { new object[] { "test@example.com", "Test@1234" } };

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async void Register_Creates_EmailToken_Test(string testEmail, string testPassword)
        {
            //Arrange
            A.CallTo(() => _userManager.CreateAsync(
                A<EComUser>.That.Matches(u => u.Email == testEmail),
                testPassword))
                .Returns(Task.FromResult(IdentityResult.Success));

            var credentials = new UserCredentialsDTO { Email = testEmail, Password = testPassword };

            //Act
            await _authService.RegisterAsync(credentials);

            //Assert
            A.CallTo(() => _userManager.GenerateEmailConfirmationTokenAsync(
                A<EComUser>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async void Login_Invokes_SignInManager_Test(string testEmail, string testPassword)
        {
            //Arrange
            A.CallTo(() => _signInManager.PasswordSignInAsync(testEmail, testPassword, false, false))
            .Returns(Task.FromResult(SignInResult.Success));

            UserCredentialsDTO credentials = new UserCredentialsDTO { Email = testEmail, Password = testPassword };

            //Act
            var result = await _authService.LoginAsync(credentials);

            //Assert
            Assert.True(result.Succeeded);
        }

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async void Register_Creates_User_With_Role_Test(string testEmail, string testPassword)
        {
            //Arrange

            A.CallTo(() => _userManager.CreateAsync(
            A<EComUser>.That.Matches(u => u.Email == testEmail),
            testPassword))
            .Returns(Task.FromResult(IdentityResult.Success));

            UserCredentialsDTO credentials = new UserCredentialsDTO { Email = testEmail, Password = testPassword };

            //Act
            IdentityResult result = await _authService.RegisterAsync(credentials);

            //Assert
            A.CallTo(() => _userManager.AddToRoleAsync(A<EComUser>.Ignored, "User"))
                .MustHaveHappenedOnceExactly();
            Assert.True(result.Succeeded);
        }

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async void Register_Sends_Email_Test(string testEmail, string testPassword)
        {
            //Arrange

            A.CallTo(() => _userManager.CreateAsync(
            A<EComUser>.That.Matches(u => u.Email == testEmail),
            testPassword))
            .Returns(Task.FromResult(IdentityResult.Success));

            UserCredentialsDTO credentials = new UserCredentialsDTO { Email = testEmail, Password = testPassword };

            //Act
            IdentityResult result = await _authService.RegisterAsync(credentials);

            //Assert
            A.CallTo(() => _emailService.SendEmailAsync(testEmail, A<string>.Ignored, A<string>.Ignored))
                .MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData("Some@mail.com", "Good-token")]
        public async void ConfirmingEmail_Invokes_UserManager_ConfirmEmail_Test(string email, string code)
        {
            //Arrange
            var fixture = new Fixture();
            var emailCredits = new EmailConfirmDTO { Email = email, ConfirmationCode = code };
            var fakeUser = new EComUser();
            var decodedToken = code.DecodeToken();
            A.CallTo(() => _userManager.FindByEmailAsync(email)).Returns(Task.FromResult(fakeUser));

            //Act
            IdentityResult result = await _authService.ConfirmEmailAsync(emailCredits);

            //Assert
            A.CallTo(() => _userManager.ConfirmEmailAsync(fakeUser, decodedToken))
                .MustHaveHappenedOnceExactly();
        }
    }
}
