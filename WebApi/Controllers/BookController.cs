using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.Applications.BookOperations.Commands.Updatebook;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController] //Attribute ile http response döneceğini taahhüt ettim.
    [Route("[controller]s")]//Gelen Requesti hangi resource ın karşılayacağı biligisini veren attribute.
    public class BookController : ControllerBase //Controllerlar ControllerBase classından kalıtım alır.
    {

        private readonly IBookStoreDbContext _context;//readonly olduğundan sadece constructor aracılığıyla assing edilebilir.readonly değişkenler uygulama içerisinden değiştirilemezler sadece constructor içerisinden set edilebilirler.

        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)//inject edilen dbcontext'i constructor aracılığıyla alabliriz.
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery Query = new  GetBooksQuery(_context,_mapper);
           var result = Query.Handle();
           return Ok(result);
        }

        [HttpGet("{id}")]//FromRoute
        public IActionResult GetById( int id)
        {
            GetBookDetailViewModel result;
                    GetBookDetailQuery Query=new GetBookDetailQuery(_context,_mapper);
                    Query.BookId=id;

                    GetBookDetailQueryValidator validator =new GetBookDetailQueryValidator();
                    validator.ValidateAndThrow(Query);

                    result=Query.Handle();
                    return Ok(result);
        }


        [HttpPost]//FromBody
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

                CreateBookCommand command =new CreateBookCommand(_context,_mapper);
                command.Model=newBook;
                command.Model.Title=command.Model.Title.Trim();//Title ın başından ve sonundan whitespaceleri sildim validation'ı strick yapmak için. 
                
                CreateBookCommandValidator validator =new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);


                command.Handle();
                return Ok();
        }

        [HttpPut("{id}")]//FromBody
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
                UpdateBookCommand command =new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;

                UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
                return Ok();
            
        }

        [HttpDelete("{id}")]//FromRoute

        public IActionResult DeleteBook(int id)
        {
               DeleteBookCommand command=new DeleteBookCommand(_context);
               command.BookId=id;
               
               DeleteBookCommandValidator validator=new  DeleteBookCommandValidator();
               validator.ValidateAndThrow(command);

               command.Handle();
               return Ok();
        }
    }
}