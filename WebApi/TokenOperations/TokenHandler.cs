using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration _configuration{get; set;}
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel=new Token();

            SymmetricSecurityKey key =new SymmetricSecurityKey (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials credantials =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = DateTime.Now.AddMinutes(15);// 15 dakikalık bir acces token yaratmak için  expiration'ı ayarladık.

            JwtSecurityToken securityToken = new  JwtSecurityToken (
                issuer :_configuration["Token:Issuer"],
                audience :_configuration["Token:Audience"],
                expires : tokenModel.Expiration,
                notBefore : DateTime.Now,
                signingCredentials : credantials
                
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            //token yaratılıyor...
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}