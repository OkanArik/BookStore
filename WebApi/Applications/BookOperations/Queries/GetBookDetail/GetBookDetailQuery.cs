using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetBookDetailViewModel Handle()
        {
            var book= _dbContext.Books.Include(x=> x.Genre).Include(x=> x.Author).Where(x=> x.Id==BookId).SingleOrDefault();

            if(book is null)
            throw new InvalidOperationException("Kitap mevcut değil.");
            
            GetBookDetailViewModel vm= _mapper.Map<GetBookDetailViewModel>(book);
            
            return vm;
        }
    }

    public class GetBookDetailViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}