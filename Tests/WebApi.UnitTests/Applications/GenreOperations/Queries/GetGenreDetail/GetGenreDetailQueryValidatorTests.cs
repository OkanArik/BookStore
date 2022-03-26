using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Application.GenreOperations.Commands.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.genreId=id;

            //act
            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError(int id)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.genreId=id;

            //act
            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            var result=validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}