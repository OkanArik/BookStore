using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            CreateAuthorCommand command =new  CreateAuthorCommand(null,null);
            command.Model= new CreateAuthorCommandModel(){
                Name="",
                Surname="",
                BirthDay=DateTime.Now.Date
            };

            //act
            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(5);


        }

        [Theory]
        [InlineData("Lord Of the","")]
        [InlineData(" ","Test")]
        [InlineData("","")]
        [InlineData("Lo","Te")]
        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string name ,string surname)
        {
            //arrange
            CreateAuthorCommand command =new  CreateAuthorCommand(null,null);
            command.Model= new CreateAuthorCommandModel(){
                Name=name,
                Surname=surname,
                BirthDay=DateTime.Now.Date.AddYears(-15)
            };

            //act
            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError() //Sadece BirtDay'i test amacıyla yazdığım test methodum.
         {
            //arrange
            CreateAuthorCommand command =new  CreateAuthorCommand(null,null);
            command.Model= new CreateAuthorCommandModel(){
                Name="testtt",
                Surname="testtttt",
                BirthDay=DateTime.Now.Date
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(1);

        }

        
        [Theory] //Happy Path
        [InlineData("Lord Of the","testttt")]
        [InlineData(" test","Test")]
        [InlineData("lord of ","lord")]
        [InlineData("Lord","Test")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name,string surname)
        {
            //arrange
            CreateAuthorCommand command =new  CreateAuthorCommand(null,null);
            command.Model= new CreateAuthorCommandModel(){
                Name=name,
                Surname=surname,
                BirthDay=DateTime.Now.Date.AddYears(-15)
            };

            //act
            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}