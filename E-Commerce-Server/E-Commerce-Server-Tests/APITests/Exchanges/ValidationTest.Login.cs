﻿using AutoFixture;
using ECom.API.Exchanges.Authentication;
using System.ComponentModel.DataAnnotations;

namespace ECom.Test.APITests.Exchanges
{
    public partial class ModelValidationTests
    {
        [Fact]
        public void LoginRequest_Validation_Null_Email_Password()
        {
            //Arrange
            string email = null;
            string password = null;
            LoginRequest request = new LoginRequest();
            var errors = new List<ValidationResult>();
            //Act
            request.Email = email;
            request.Password = password;
            Validator.TryValidateObject(request, new ValidationContext(request), errors);
            //Assert
            Assert.Equal(2, errors.Count);
        }

        [Fact]
        public void LoginRequest_Validation_Short_Password()
        {
            //Arrange
            var fixture = new Fixture();
            LoginRequest request = new LoginRequest()
            {
                Email = fixture.Create<string>(),
                Password = fixture.Create<string>().Substring(0, 5)
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            //Assert
            Assert.Contains("Password", failedMembers);
        }

        [Theory]
        [MemberData(nameof(weakPasswords))]
        public void LoginRequest_Validation_Weak_Password(string password)
        {
            //Arrange
            var fixture = new Fixture();
            LoginRequest request = new LoginRequest()
            {
                Email = fixture.Create<string>(),
                Password = password
            };
            var errors = new List<ValidationResult>();

            //Act
            Validator.TryValidateObject(request, new ValidationContext(request), errors, true);
            List<string> failedMembers = errors.SelectMany(x => x.MemberNames).ToList();
            Assert.True(failedMembers.Contains("Password"));
        }

        [Theory]
        [MemberData(nameof(strongPasswords))]
        public void LoginRequest_Validation_Strong_Password(string password)
        {
            //Arrange
            var fixture = new Fixture();
            LoginRequest request = new LoginRequest()
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

        public static IEnumerable<object[]> weakPasswords =>
        new List<object[]>
        {
            new object[]{"aE114291" }, new object[] { "idziak" },new object[] { "idzanagi" },
            new object[] { "idyxgsb3" },new object[] { "sDYlNjGzkchwA" },new object[] { "rdx-1101" },
            new object[] { "i-dwsos2" },new object[] { "Rduras62" },new object[] { "Idtyktmot2" },
            new object[] { "idunn0" }
        };

        public static IEnumerable<object[]> strongPasswords =>
        new List<object[]>
        {
            new object[]{"5eF#!VKK&6FW" }, new object[] { "R4j-ePTxJ&<x" },new object[] { "T+0#&x5QH}RI" },
            new object[] { "Nk62y{g05_9J" },new object[] { ".a1`EtC}~6xN" },new object[] { "1Iyn|9Y(*Ua~" },
            new object[] { "G?YDm%!4Oh`C" },new object[] { "0mV;L7qIFw%j" },new object[] { "Uu0,#cLB_{Iy" },
            new object[] { "O>Rvtk_9ZmPM" }
        };

        public static IEnumerable<object[]> wrongEmails =>
        new List<object[]>
        {
            new object[]{"kaplins" }, new object[] { "empry@" },new object[] { "@wrong.com" },
            new object[] { "wrong@.com" },new object[] { "wrong@com" },new object[] { "wrong.com" }
        };

        public static IEnumerable<object[]> correctEmails =>
        new List<object[]>
        {
            new object[]{"correct@mail.com" },new object[] { "Mistes.Sker@mail.com" },
            new object[] { "correct32@kol.mu" }
        };

    }
}
