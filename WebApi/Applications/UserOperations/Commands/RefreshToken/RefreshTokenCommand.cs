using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.TokenOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string refreshToken { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x=> x.RefreshToken==refreshToken && x.RefreshTokenExpireDate> DateTime.Now);

            if(user is not null)
            {
                //Token Yarat
                TokenHandler handler = new TokenHandler(_configuration); 
                Token token=handler.CreateAccessToken(user);

                user.RefreshToken= token.RefreshToken;
                user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);

                _dbContext.SaveChanges();

                return token;
            }
            else 
              throw new InvalidOperationException("Valid bir refresh token bulunamad─▒!");
        }
    }
}
