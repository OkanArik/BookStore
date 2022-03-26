using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Applications.BookOperations.Commands.DeleteBook
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
        public void WhenNoneExistenttBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            //arrange : hazırlık 
            DeleteBookCommand command=new DeleteBookCommand(_context);
            command.BookId=bookId;
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");
        }

        [Theory]  //Happy path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted(int bookId)
        {
            //arrange
            DeleteBookCommand command=new DeleteBookCommand(_context);
            command.BookId=bookId;
            
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x=> x.Id==command.BookId);
            book.Should().BeNull();
        }
    }
}