using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context =testFixture.Context;
            _mapper =testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange : hazırlık 
            var genre = new  Genre(){
                Name ="WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command=new CreateGenreCommand(_context);
            command.Model = new CreateGenreCommandModel(){
                Name="WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            //act : çalıştırma && assert : doğrulama
            FluentActions
                         .Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre zaten mevcut.");
        }

        [Fact]  //Happy path
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreCommandModel model= new CreateGenreCommandModel (){Name = "Noval"};
            command.Model=model;
            
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x=> x.Name==model.Name);
            genre.Should().NotBeNull();
        }
    }
}