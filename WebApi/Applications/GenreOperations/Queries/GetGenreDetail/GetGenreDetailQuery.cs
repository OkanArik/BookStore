using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;


namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int genreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetGenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Id==genreId && x.IsActive==true);

            if(genre is null)
              throw new InvalidOperationException("Genre mevcut değil.");
          
            return _mapper.Map<GetGenreDetailViewModel>(genre);
        }

    }

    public class GetGenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}