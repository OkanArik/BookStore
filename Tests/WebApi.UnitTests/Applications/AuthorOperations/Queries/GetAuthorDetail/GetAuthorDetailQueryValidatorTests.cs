using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Application.AuthorOperations.Commands.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnError(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId=authorId;

            //act
            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnError(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId=authorId;

            //act
            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}