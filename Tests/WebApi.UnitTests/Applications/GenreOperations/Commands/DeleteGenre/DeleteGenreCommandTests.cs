using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context =testFixture.Context;
        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenNoneExistentGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int id)
        {
            //arrange : hazırlık 
            DeleteGenreCommand command=new DeleteGenreCommand(_context);
            command.genreId=id;
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre mevcut değil.");
        }

        [Theory]  //Happy path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted(int id)
        {
            //arrange
            DeleteGenreCommand command=new DeleteGenreCommand(_context);
            command.genreId=id;
            
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x=> x.Id==command.genreId);
            genre.Should().BeNull();
        }
    }
}