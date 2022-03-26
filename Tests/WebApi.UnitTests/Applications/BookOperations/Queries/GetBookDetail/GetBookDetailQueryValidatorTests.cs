using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookId;

            //act
            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookId;

            //act
            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}