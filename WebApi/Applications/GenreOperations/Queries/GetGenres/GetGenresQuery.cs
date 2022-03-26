using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Queries.GetGenre
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GetGenresViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(x => x.IsActive==true).OrderBy(x=> x.Id).ToList<Genre>();

            List<GetGenresViewModel>  returnObj = _mapper.Map<List<GetGenresViewModel>>(genres);
             
            return returnObj;
        }

    }

    public class GetGenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}