using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context =testFixture.Context;
            _mapper =testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange : hazırlık 
            var book = new  Book(){
                Title ="WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount =100,
                AuthorId=1,
                PublishDate = new DateTime(1990,01,10),
                GenreId=2
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){
                Title="WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]  //Happy path
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookModel model= new CreateBookModel (){Title="Hobit",PageCount=1000,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1,AuthorId=1};
            command.Model=model;
            
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x=> x.Title==model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);

        }
    }
}