using FluentValidation;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        public const string FullNameRequiredMsg = "FullName is required.";
        public const string EmailRequiredMsg = "Email is required.";
        public const string UserNameRequiredMsg = "Username is required.";
        public SaveUserCommandValidator()
        {
            RuleFor(cmd => cmd.FullName).NotEmpty().WithMessage(FullNameRequiredMsg);
            RuleFor(cmd => cmd.Email).NotEmpty().WithMessage(EmailRequiredMsg);
            RuleFor(cmd => cmd.UserName).NotEmpty().WithMessage(UserNameRequiredMsg);
        }
    }
}