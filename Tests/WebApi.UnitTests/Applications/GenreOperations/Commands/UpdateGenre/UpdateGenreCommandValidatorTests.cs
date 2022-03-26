using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            UpdateGenreCommand command =new  UpdateGenreCommand(null);
            command.Model= new UpdateGenreCommandModel(){
                Name="n"
            };
            command.genreId=0;

            //act
            UpdateGenreCommandValidator validator=new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(2);


        }

        [Theory]
        [InlineData("",0)]
        [InlineData("n",1)]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string name, int id)
        {
            //arrange
            UpdateGenreCommand command =new  UpdateGenreCommand(null);
            command.Model= new UpdateGenreCommandModel(){
                Name=name
            };
            command.genreId=id;

            //act
            UpdateGenreCommandValidator validator=new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }


        [Fact] //Happy path
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateGenreCommand command =new  UpdateGenreCommand(null);
            command.Model= new UpdateGenreCommandModel(){
                Name="Noval"
            };
            command.genreId=1;
            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}