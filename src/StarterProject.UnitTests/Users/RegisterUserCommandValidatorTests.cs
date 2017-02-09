using FluentAssertions;
using NUnit.Framework;
using StarterProject.WebApi.Commands.Users;

namespace StarterProject.UnitTests.Users
{
    [TestFixture]
    public class RegisterUserCommandValidatorTests
    {
        RegisterUserCommandValidator validator = new RegisterUserCommandValidator();

        [Test]
        public void FullNameIsRequired()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = string.Empty,
                UserName = "halcharger",
                Email = "allen.firth@gmail.com"
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == RegisterUserCommandValidator.FullNameRequiredMsg);
        }

        [Test]
        public void EmailIsRequired()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = "allen firth",
                UserName = "halcharger",
                Email = string.Empty
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == RegisterUserCommandValidator.EmailRequiredMsg);
        }

        [Test]
        public void UsernameIsRequired()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = "allen firth",
                Email = "allen.firth@gmail.com", 
                UserName = string.Empty
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == RegisterUserCommandValidator.UserNameRequiredMsg);
        }

        [Test]
        public void PasswordIsRequired()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = "allen firth",
                Email = "allen.firth@gmail.com",
                UserName = "halcharger"
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == RegisterUserCommandValidator.PasswordRequiredMsg);
        }
    }
}