using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.Updatebook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 12;
            //act & assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");
        }

        [Fact]

        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Hobbit",
                GenreId = 2,
                AuthorId=4,
                PageCount=9999
            };

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 2;
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);

            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
            book.PageCount.Should().Be(model.PageCount);

        }
    }
}