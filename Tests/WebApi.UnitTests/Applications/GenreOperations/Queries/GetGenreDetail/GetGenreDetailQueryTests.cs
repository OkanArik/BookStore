using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory] 
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int Id)
        {
            //arrange
            GetGenreDetailQuery query=new GetGenreDetailQuery(_context,_mapper);
            query.genreId=Id;

            //act & assert
            FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre mevcut değil.");

        }

        [Theory] //Happy Path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldNotBeReturnError(int id)
        {
            //arrange
            GetGenreDetailQuery query=new GetGenreDetailQuery(_context,_mapper);
            query.genreId=id;

            //act
            GetGenreDetailViewModel vm=new GetGenreDetailViewModel();
            FluentActions.Invoking(()=>vm = query.Handle()).Invoke();

            //Assert

            var genre= _context.Genres.SingleOrDefault(x=> x.Id==query.genreId);

            vm.Name.Should().Be(genre.Name);
        }
    }
}