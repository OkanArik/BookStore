using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.BirthDay).LessThan(DateTime.Now.Date.AddYears(-10));
        }
    }
}