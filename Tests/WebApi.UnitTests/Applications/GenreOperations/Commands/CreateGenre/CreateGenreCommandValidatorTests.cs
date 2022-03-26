using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            CreateGenreCommand command =new  CreateGenreCommand(null);
            command.Model= new CreateGenreCommandModel(){
               Name=""
            };

            //act
            CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(2);


        }

        [Theory]
        [InlineData("NA")]
        [InlineData("")]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string name )
        {
            //arrange
            CreateGenreCommand command =new  CreateGenreCommand(null);
            command.Model= new CreateGenreCommandModel(){
                Name=name,
            };

            //act
            CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

        [Fact] //Happy path
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateGenreCommand command =new  CreateGenreCommand(null);
            command.Model= new CreateGenreCommandModel(){
                Name="Noval"
            };
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}