using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using ECom.BLogic.Services.Models;
using ECom.BLogic.Services.Authentication;

namespace ECom.Test.BLogicTests
{
    public class AuthServiceTests
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private AuthService _authService;

        public AuthServiceTests()
        {
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _signInManager = A.Fake<SignInManager<IdentityUser>>();
            _authService = new AuthService(_signInManager, _userManager);
        }
        public static IEnumerable<object[]> userCredentials =>
            new List<object[]>{ new object[] { "test@example.com", "Test@1234" } };


        [Fact]
        
        public void Login_IncorrectCredentials_Test()
        {
        }

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async void Login_Invokes_SignInManager_Test(string testEmail,string testPassword)
        {
            //Arrange
            A.CallTo(() => _signInManager.PasswordSignInAsync(testEmail,testPassword,false,false))
            .Returns(Task.FromResult(SignInResult.Success));

            UserCredentials credentials = new UserCredentials { Email = testEmail, Password = testPassword };

            //Act
            var result = await _authService.LoginAsync(credentials);

            //Assert 
            Assert.True(result.Succeeded);
        }

        [Fact]
        public void Register_IncorrectCredentials_Test()
        {
            
        }

        [Theory]
        [MemberData(nameof(userCredentials))]
        public async  void Register_Invokes_UserManager_Create_Test(string testEmail, string testPassword)
        {
            //Arrange

            A.CallTo(() => _userManager.CreateAsync(
            A<IdentityUser>.That.Matches(u => u.Email == testEmail),
            testPassword))
            .Returns(Task.FromResult(IdentityResult.Success));

            UserCredentials credentials = new UserCredentials { Email = testEmail, Password = testPassword };
            
            //Act
             IdentityResult result = await _authService.RegisterAsync(credentials);

            //Assert 
            Assert.True(result.Succeeded);
        }
        [Fact]
        public void Confirming_Password_Test()
        {

        }
    }
}
