using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.Updatebook;
using Xunit;

namespace Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            UpdateBookCommand command =new  UpdateBookCommand(null);
            command.Model= new UpdateBookModel(){
                Title="",
                PageCount=0,
                GenreId=0,
                AuthorId=0
            };
            command.BookId=0;

            //act
            UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(6);


        }

        [Theory]
        [InlineData("Lord Of the",0,0,0,1)]
        [InlineData("Lord Of the",0,0,1,0)]
        [InlineData("",0,1,0,-1)]
        [InlineData("Lord Of the",1,0,0,-2)]
        [InlineData("Ld Of the",1,0,1,-3)]
        [InlineData("Lo",0,0,0,-4)]
        [InlineData("    ",1,1,1,-5)]
        [InlineData(" Lord  ",1,1,0,-6)]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string title ,int pageCount , int genreId , int authorId ,int bookid)
        {
            //arrange
            UpdateBookCommand command =new  UpdateBookCommand(null);
            command.Model= new UpdateBookModel(){
                Title=title,
                PageCount=pageCount,
                GenreId=genreId,
                AuthorId=authorId
            };
            command.BookId=bookid;

            //act
            UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }


        [Fact] //Happy path
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command =new  UpdateBookCommand(null);
            command.Model= new UpdateBookModel(){
                Title="title",
                PageCount=100,
                GenreId=1,
                AuthorId=1
            };
            command.BookId=1;
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}