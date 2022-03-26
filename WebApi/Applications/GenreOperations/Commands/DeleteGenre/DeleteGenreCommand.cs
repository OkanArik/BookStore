using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int genreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=>x.Id==genreId);

            if(genre is null)
              throw new InvalidOperationException("Genre mevcut değil.");

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}