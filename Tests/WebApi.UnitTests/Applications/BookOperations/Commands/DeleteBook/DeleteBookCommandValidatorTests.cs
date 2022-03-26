using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteBookCommand command =new  DeleteBookCommand(null);
            command.BookId=0;

            //act
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(1);


        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-4)]
        [InlineData(-5)]
        [InlineData(-6)]
        [InlineData(-7)]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnError(int bookId)
        {
            //arrange
            DeleteBookCommand command =new  DeleteBookCommand(null);
            command.BookId=bookId;

            //act
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(1);

        }

        [Theory] //Happy path
        [InlineData(8)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int bookId)
        {
            //arrange
            DeleteBookCommand command =new  DeleteBookCommand(null);
            command.BookId=bookId;
            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}