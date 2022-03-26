using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GetAuthorsQueryViewModel> Handle()
        {
            var authorList= _dbContext.Authors.OrderBy(x=> x.Id).ToList<Author>();
        
           List<GetAuthorsQueryViewModel> vm = _mapper.Map<List<GetAuthorsQueryViewModel>>(authorList);

           return vm;
            
        }
    }

    public class GetAuthorsQueryViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string  BirthDay { get; set; }
    }
}