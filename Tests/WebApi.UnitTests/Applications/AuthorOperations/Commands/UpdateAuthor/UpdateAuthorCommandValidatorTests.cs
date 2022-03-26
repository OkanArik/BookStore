using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange
            UpdateAuthorCommand command =new  UpdateAuthorCommand(null,null);
            command.Model= new UpdateAuthorCommandModel(){
                Name="",
                Surname=""
            };
            command.AuthorId=0;

            //act
            UpdateAuthorCommandValidator validator=new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(5);


        }

        [Theory]
        [InlineData("Okan","",0)]
        [InlineData("Ok","",1)]
        [InlineData("","Arık",-2)]

        public void WhenInvalidInputAreGivens_Validator_ShouldBeReturnErrors(string name ,string surname ,int authorId)
        {
            //arrange
            UpdateAuthorCommand command =new  UpdateAuthorCommand(null,null);
            command.Model= new UpdateAuthorCommandModel(){
                Name=name,
                Surname=surname
            };
            command.AuthorId=authorId;

            //act
            UpdateAuthorCommandValidator validator=new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);


        }


        [Fact] //Happy path
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            //arrange
            UpdateAuthorCommand command =new  UpdateAuthorCommand(null,null);
            command.Model= new UpdateAuthorCommandModel(){
                Name="Okan",
                Surname="Arık"
            };
            command.AuthorId=1;
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result =validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}