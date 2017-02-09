using FluentValidation;

namespace StarterProject.WebApi.Commands.Users
{
    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        public const string FullNameRequiredMsg = "FullName is a required field.";
        public const string EmailRequiredMsg = "Email is a required field.";
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(cmd => cmd.FullName).NotEmpty().WithMessage(FullNameRequiredMsg);
            RuleFor(cmd => cmd.Email).NotEmpty().WithMessage(EmailRequiredMsg);
        }
    }
}