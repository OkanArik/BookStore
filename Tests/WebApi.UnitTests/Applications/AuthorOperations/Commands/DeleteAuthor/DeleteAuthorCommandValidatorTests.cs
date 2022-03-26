using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteAuthorCommand command =new  DeleteAuthorCommand(null);
            command.AuthorId=0;

            //act
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
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
        public void WhenInvalidInputsAreGivens_Validator_ShouldBeReturnError(int bookId)
        {
            //arrange
            DeleteAuthorCommand command =new  DeleteAuthorCommand(null);
            command.AuthorId=0;

            //act
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
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
            DeleteAuthorCommand command =new  DeleteAuthorCommand(null);
            command.AuthorId=0;

            //act
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(1);

        }
    }
}