using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = 12;
            //act & assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre mevcut değil.");
        }

        [Fact]

        public void WhenValidGenreIdIsGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommandModel model = new UpdateGenreCommandModel()
            {
                Name = "Noval"
            };

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = 2;
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == command.genreId);

            genre.Name.Should().Be(model.Name);

        }
    }
}