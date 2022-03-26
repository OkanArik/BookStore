using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.DBOperations;

//Terminal Commands:
//dotnet new webapi -n WebApi framework net5.0 
//dotnet new sln -n BookStoreSln
//dotnet sln add WebApi
//dotnet add package Microsoft.EntityFrameworkCore --version 5.0.6
//dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.6  
//dotnet add package AutoMapper --version 10.1.1
//dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1
//dotnet add package FluentValidation
//dotnet add package Newtonsoft.json
//dotnet new xunit -n WebApi.UnitTests
//dotnet sln add Tests/WebApi.UnitTests
//dotnet add reference ../../WebApi 
//dotnet add package Moq --version 4.16.1
//dotnet add package FluentAssertions --version 5.10.3
//cd ..
//cd ..
//dotnet clean
//dotnet build 
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 5.0.6
//

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host =CreateHostBuilder(args).Build();

            using ( var scope = host.Services.CreateScope()) //Program her ayağa kalktığında alışacak .
            {
                var services = scope.ServiceProvider;//services adında ServiceProvider yarattım.
                DataGenerator.Initialize(services);//Initialize methoduma services i geçerek çalıştırdım.
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
