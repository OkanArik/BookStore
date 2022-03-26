using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext; //Burada kullancam databasecontext ve bunu constructordan alcam.
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GetBooksViewModel> Handle() //işi yapacak olan methodum.
        {
             var bookList = _dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x=> x.Id).ToList<Book>();

             List<GetBooksViewModel> vm = _mapper.Map<List<GetBooksViewModel>>(bookList);
             
             return vm;
        }

        public class GetBooksViewModel
        {
             public string Title { get; set; }
             public string  Author { get; set; }
             public int PageCount { get; set; }
             public string  PublishDate { get; set; }
             public string Genre { get; set; }

        }
        
    }
}