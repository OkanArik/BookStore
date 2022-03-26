using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            DeleteGenreCommand command =new  DeleteGenreCommand(null);
            command.genreId=0;

            //act
            DeleteGenreCommandValidator validator=new DeleteGenreCommandValidator();
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
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnError(int GenreId)
        {
            //arrange
            DeleteGenreCommand command =new  DeleteGenreCommand(null);
            command.genreId=GenreId;

            //act
            DeleteGenreCommandValidator validator=new DeleteGenreCommandValidator();
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
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int GenreId)
        {
            //arrange
            DeleteGenreCommand command =new  DeleteGenreCommand(null);
            command.genreId=GenreId;
            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}