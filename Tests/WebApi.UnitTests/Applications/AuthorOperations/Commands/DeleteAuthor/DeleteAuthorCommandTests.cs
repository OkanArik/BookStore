using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
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
        public void WhenNoneExistentAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //arrange : hazırlık 
            DeleteAuthorCommand command=new DeleteAuthorCommand(_context);
            command.AuthorId=authorId;
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author mevcut değil.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenAuthorHasABook_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //arrange : hazırlık 
            DeleteAuthorCommand command=new DeleteAuthorCommand(_context);
            command.AuthorId=authorId;
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitabı yayında olan yazar silinemez.");
        }

        [Theory]  //Happy path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted(int authorId)
        {
            //arrange
            DeleteAuthorCommand command=new DeleteAuthorCommand(_context);
            command.AuthorId=authorId;
            
            DeleteBookCommand commandBook= new DeleteBookCommand(_context);//DeleteAuthorCommand file ımda kitabı yayında olan Author silinemez logiz ini koyduğumdan , burada yazar silme testi için silmek istediğim yazarın önce yayındaki kitabını sildim.
            commandBook.BookId=authorId;
            commandBook.Handle();
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x=> x.Id==authorId);
            author.Should().BeNull();
        }
    }
}