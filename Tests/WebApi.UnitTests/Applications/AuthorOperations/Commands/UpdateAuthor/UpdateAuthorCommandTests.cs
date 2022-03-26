using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = 12;
            //act & assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author mevcut değil.");
        }

        [Fact] //Happy Path
        public void WhenValidAuthorIdIsGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateAuthorCommandModel model = new UpdateAuthorCommandModel()
            {
                Name = "Hobbit",
                Surname = "Lors"
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = 2;
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);

        }
    }
}