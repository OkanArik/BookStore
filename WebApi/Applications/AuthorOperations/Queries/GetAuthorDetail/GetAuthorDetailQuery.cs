using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public GetAuthorDetailQueryViewModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x=> x.Id==AuthorId);

            if(author is null)
              throw new InvalidOperationException("Author mevcut değil.");
            
            return _mapper.Map<GetAuthorDetailQueryViewModel>(author);
        }
        

    }

    public class GetAuthorDetailQueryViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDay { get; set; }

    }
}