using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using Xunit;

namespace Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            CreateBookCommand command =new  CreateBookCommand(null,null);
            command.Model= new CreateBookModel(){
                Title="",
                PageCount=0,
                PublishDate=DateTime.Now.Date,
                GenreId=0,
                AuthorId=0
            };

            //act
            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

        [Theory]
        [InlineData("Lord Of the",0,0,0)]
        [InlineData("Lord Of the",0,0,1)]
        [InlineData("",0,1,0)]
        [InlineData("Lord Of the",1,0,0)]
        [InlineData("Ld Of the",1,0,1)]
        [InlineData("Lo",0,0,0)]
        [InlineData("    ",1,1,1)]
        [InlineData(" Lord  ",1,1,0)]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string title ,int pageCount , int genreId , int authorId)
        {
            //arrange
            CreateBookCommand command =new  CreateBookCommand(null,null);
            command.Model= new CreateBookModel(){
                Title=title,
                PageCount=pageCount,
                PublishDate =DateTime.Now.Date.AddYears(-1),
                GenreId=genreId,
                AuthorId=authorId
            };

            //act
            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateBookCommand command =new  CreateBookCommand(null,null);
            command.Model= new CreateBookModel(){
                Title="title",
                PageCount=100,
                PublishDate =DateTime.Now.Date,
                GenreId=1,
                AuthorId=1
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact] //Happy path
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateBookCommand command =new  CreateBookCommand(null,null);
            command.Model= new CreateBookModel(){
                Title="title",
                PageCount=100,
                PublishDate =DateTime.Now.Date.AddYears(-10),
                GenreId=1,
                AuthorId=1
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}