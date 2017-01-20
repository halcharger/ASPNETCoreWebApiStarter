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
                Email = "allen.firth@gmail.com"
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == SaveUserCommandValidator.FullNameRequiredMsg);
        }

        [Test]
        public void EmailIsREquired()
        {
            var cmd = new SaveUserCommand
            {
                FullName = "john dow",
                Email = string.Empty
            };

            var result = validator.Validate(cmd);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == SaveUserCommandValidator.EmailRequiredMsg);
        }
    }
}