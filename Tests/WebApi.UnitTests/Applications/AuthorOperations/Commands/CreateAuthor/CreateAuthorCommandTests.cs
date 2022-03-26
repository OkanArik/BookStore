using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context =testFixture.Context;
            _mapper =testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange : hazırlık 
            var author = new  Author(){
                Name="Test 1",
                Surname="Test 2",
                BirthDay= new DateTime(1999,01,10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command=new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorCommandModel(){
                Name=author.Name,
                Surname=author.Surname,
                BirthDay= author.BirthDay
            };
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>()
                         .And.Message.Should().Be("Author zaten mevcut.");
        }

        [Fact]  //Happy path
        public void WhenValidInputsIsGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorCommandModel model= new CreateAuthorCommandModel (){
                Name="Test3",
                Surname="Test4",
                BirthDay= new DateTime(1998,01,10)
            };
            command.Model=model;
            
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x=> x.Name==model.Name && x.Surname==model.Surname && x.BirthDay==model.BirthDay);
            author.Should().NotBeNull();
        }
    }
}