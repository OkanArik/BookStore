using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model{get; set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Title==Model.Title);

            if(book is not null)
              throw new InvalidOperationException("Kitap zaten mevcut.");

            book= _mapper.Map<Book>(Model);//Model objesi ile gelen veriyi Book objesine mapledik.

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}