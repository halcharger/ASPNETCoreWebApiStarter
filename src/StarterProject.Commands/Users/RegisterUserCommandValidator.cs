using FluentValidation;

namespace StarterProject.Commands.Users
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public const string FullNameRequiredMsg = "FullName is required.";
        public const string EmailRequiredMsg = "Email is required.";
        public const string UserNameRequiredMsg = "Username is required.";
        public const string PasswordRequiredMsg = "Password is required.";

        public RegisterUserCommandValidator()
        {
            RuleFor(cmd => cmd.FullName).NotEmpty().WithMessage(FullNameRequiredMsg);
            RuleFor(cmd => cmd.Email).NotEmpty().WithMessage(EmailRequiredMsg);
            RuleFor(cmd => cmd.UserName).NotEmpty().WithMessage(UserNameRequiredMsg);
            RuleFor(cmd => cmd.Password).NotEmpty().WithMessage(PasswordRequiredMsg);
        }
    }
}