using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private IBookStoreDbContext _dbContext;

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
             var book = _dbContext.Books.SingleOrDefault(x=> x.Id==BookId);

            if(book is null)
              throw new InvalidOperationException("Kitap mevcut değil.");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}