using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.Commands.Updatebook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }

        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id==BookId);

            if(book is null)
              throw new InvalidOperationException("Kitap mevcut değil.");

            book.Title = Model.Title!=default ? Model.Title : book.Title;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.GenreId = Model.GenreId!= default ? Model.GenreId : book.GenreId;
            book.AuthorId=Model.AuthorId!=default? Model.AuthorId : book.AuthorId;
            //book.PublishDate= Model.PublishDate!=default?Model.PublishDate:book.PublishDate;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        //public DateTime PublishDate { get; set; }//PublishDate dışarıdan tarafından güncellenemesin dedik ve kappattık.
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
    }
}