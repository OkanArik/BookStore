using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory] 
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query=new GetAuthorDetailQuery(_context,null);
            query.AuthorId=authorId;

            //act & assert
            FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author mevcut değil.");

        }

        [Theory] //Happy Path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenVlidInputIsGiven_InvalidOperationException_ShouldNotBeReturnError(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query=new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId=authorId;

            //act
            GetAuthorDetailQueryViewModel vm=new GetAuthorDetailQueryViewModel();
            FluentActions.Invoking(()=>vm = query.Handle()).Invoke();

            //Assert

            var author= _context.Authors.SingleOrDefault(x=> x.Id==query.AuthorId);

            vm.Name.Should().Be(author.Name);
            vm.Surname.Should().Be(author.Surname);
            vm.BirthDay.Should().Be(author.BirthDay.ToString("dd-MM-yyyy"));
        }
    }
}