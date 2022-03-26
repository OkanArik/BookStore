using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(author=> author.Id==AuthorId);
            var book = _dbContext.Books.FirstOrDefault(book=>book.AuthorId==AuthorId);

            if(author is null)
              throw new InvalidOperationException("Author mevcut değil.");

            if(_dbContext.Books.FirstOrDefault(book=> book.AuthorId==author.Id) is not null)
              throw new InvalidOperationException("Kitabı yayında olan yazar silinemez.");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}