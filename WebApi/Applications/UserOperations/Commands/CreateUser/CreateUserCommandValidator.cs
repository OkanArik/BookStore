using System;
using FluentValidation;

namespace WebApi.Applications.USerOperations.Commands.CreateUSer
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>//Burda AbstractValidator dan kalıtım aldırarark CetaBookCommandValidator sınıfı CreateBookCommand ın objelerini valide eder dedim.
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command=> command.Model.Name.Trim()).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Surname.Trim()).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Email.Trim()).NotEmpty().MinimumLength(12);
            RuleFor(command=> command.Model.Password.Trim()).NotEmpty().MinimumLength(6);
        }
    }
}