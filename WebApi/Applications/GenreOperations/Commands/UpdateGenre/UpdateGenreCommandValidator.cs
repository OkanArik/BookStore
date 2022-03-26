using FluentValidation;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=> command.genreId).GreaterThan(0);
            RuleFor(command=> command.Model.Name).MinimumLength(3).When(x=> x.Model.Name.Trim()!=string.Empty);
        }
    }
}