﻿using FluentValidation;
using FluentValidation.Results;
using NExtensions.Core;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        public const string FullNameRequiredMsg = "FullName is required.";
        public const string EmailRequiredMsg = "Email is required.";
        public const string UserNameRequiredMsg = "Username is required.";
        public const string PasswordRequiredMsg = "Password is required.";
        public SaveUserCommandValidator()
        {
            RuleFor(cmd => cmd.FullName).NotEmpty().WithMessage(FullNameRequiredMsg);
            RuleFor(cmd => cmd.Email).NotEmpty().WithMessage(EmailRequiredMsg);
            RuleFor(cmd => cmd.UserName).NotEmpty().WithMessage(UserNameRequiredMsg);
            Custom(PasswordIsRequiredWhenUserIsNew);
        }

        private ValidationFailure PasswordIsRequiredWhenUserIsNew(SaveUserCommand cmd)
        {
            return cmd.Id.IsNullOrEmpty() && cmd.Password.IsNullOrEmpty()
                ? new ValidationFailure("Password", PasswordRequiredMsg)
                : null;
        }
    }
}