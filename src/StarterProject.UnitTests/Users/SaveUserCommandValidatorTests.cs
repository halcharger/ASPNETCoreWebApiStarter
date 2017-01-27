using System;
using FluentAssertions;
using NUnit.Framework;
using StarterProject.Commands.Users;

namespace StarterProject.UnitTests.Users
{
    [TestFixture]
    public class SaveUserCommandValidatorTests
    {
        SaveUserCommandValidator validator = new SaveUserCommandValidator();

        [Test]
        public void FullNameIsRequired()
        {
            var cmd = new SaveUserCommand
            {
                FullName = string.Empty,
                UserName = "halcharger",
                Email = "allen.firth@gmail.com"
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == SaveUserCommandValidator.FullNameRequiredMsg);
        }

        [Test]
        public void EmailIsRequired()
        {
            var cmd = new SaveUserCommand
            {
                FullName = "allen firth",
                UserName = "halcharger",
                Email = string.Empty
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == SaveUserCommandValidator.EmailRequiredMsg);
        }

        [Test]
        public void UsernameIsRequired()
        {
            var cmd = new SaveUserCommand
            {
                FullName = "allen firth",
                Email = "allen.firth@gmail.com", 
                UserName = string.Empty
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == SaveUserCommandValidator.UserNameRequiredMsg);
        }
    }
}