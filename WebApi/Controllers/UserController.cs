using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.TokenOperations.Commands.CreateToken;
using WebApi.Applications.TokenOperations.Commands.RefreshToken;
using WebApi.Applications.USerOperations.Commands.CreateUSer;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{   
    [ApiController]
    [Route("[Controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        private readonly IConfiguration  _configuration;//app.settings altındaki configlere erişmemi sağlıyor.

        public UserController (IBookStoreDbContext context,IConfiguration configuration,IMapper mapper)
        {
            _context=context;
            _configuration=configuration;
            _mapper=mapper;
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = newUser;

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel Login)
        {
            CreateTokenCommand command= new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model=Login;

            var token= command.Handle();

            return token;
        }

         [HttpGet("refreshtoken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command=new RefreshTokenCommand(_context,_configuration);
            command.refreshToken  = token;
            
            var resultToken = command.Handle();

            return resultToken;
        }
    }
}