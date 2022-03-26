using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int genreId { get; set; }
        public UpdateGenreCommandModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Id==genreId);

            if(genre is null)
              throw new InvalidOperationException("Genre mevcut değil.");

            if (_dbContext.Genres.Any(x=> x.Name.ToLower()==Model.Name.ToLower() && x.Id!= genreId))
              throw new InvalidOperationException("Güncellemek istediğin yeni genre zaten mevcut!");
            
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name:Model.Name;
            genre.IsActive= Model.IsActive;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreCommandModel 
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } =true;
    }
}