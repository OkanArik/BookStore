using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query =new GetGenresQuery(_dbContext,_mapper);
            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext,_mapper);
            query.genreId=id;

            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreCommandModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext);
            command.Model=newGenre;

            CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreCommandModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.genreId=id;
            command.Model=updatedGenre;

            UpdateGenreCommandValidator validator=new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
        
        [HttpDelete ("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.genreId=id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}