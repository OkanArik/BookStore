using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory] 
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            //arrange
            GetBookDetailQuery query=new GetBookDetailQuery(_context,null);
            query.BookId=bookId;

            //act & assert
            FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");

        }

        [Theory] //Happy Path
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenVlidInputIsGiven_InvalidOperationException_ShouldNotBeReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
            query.BookId=bookId;

            //act
            GetBookDetailViewModel vm=new GetBookDetailViewModel();
            FluentActions.Invoking(()=>vm = query.Handle()).Invoke();

            //Assert

            var book= _context.Books.SingleOrDefault(x=> x.Id==query.BookId);

            vm.Title.Should().Be(book.Title);
            vm.PageCount.Should().Be(book.PageCount);
            vm.PublishDate.Should().Be(book.PublishDate.ToString("dd-MM-yyyy"));
            vm.Genre.Should().NotBeNullOrEmpty();
            vm.Author.Should().NotBeNullOrEmpty();
        }
    }
}