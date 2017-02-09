using FluentValidation;

namespace StarterProject.WebApi.Commands.Users
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(cmd => cmd.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(cmd => cmd.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}