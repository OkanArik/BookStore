using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreCommandModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public CreateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Name==Model.Name);

            if(genre is not null)
              throw new InvalidOperationException("Genre zaten mevcut.");

            genre = new Genre();
            genre.Name=Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreCommandModel 
    {
        public string Name { get; set; }
    }
}