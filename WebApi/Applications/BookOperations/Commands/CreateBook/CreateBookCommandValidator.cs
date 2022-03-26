using System;
using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>//Burda AbstractValidator dan kalıtım aldırarark CetaBookCommandValidator sınıfı CreateBookCommand ın objelerini valide eder dedim.
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0);
            RuleFor(command=> command.Model.PageCount).GreaterThan(0);
            RuleFor(command=> command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date.AddDays(-1));
            RuleFor(command=> command.Model.Title).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.AuthorId).GreaterThan(0);

        }
    }
}