using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
                opt.TokenValidationParameters = new TokenValidationParameters{
                    ValidateAudience = true, // istemci kitlesini valide et
                    ValidateIssuer = true,//Token'ın sağlayıcısının validasyonunu yap
                    ValidateLifetime = true,//token ın lifetime ını valide et
                    ValidateIssuerSigningKey = true ,//tokenı kiriptolayacağımız key i valide et
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero  //Token'ı üreten sunucunun var olduğu time zone ile kullanıcıların time zone ı birbirinden farklı olduğu durumlarda , zamanı ayarlamak için kullanılır.Şu anda bizim böyle bir concern imiz olmadığından zore verdik.
                };
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            //DataBase bizim için servis olduğundan uygulama içerisinde kullanmak için burada onu implement etcez yani uygulamaya göstercez.

            services.AddDbContext<BookStoreDbContext>(options=> options.UseInMemoryDatabase(databaseName:"BookStoreDB"));//Uygulamamızın istediğimiz yerinden artık BookStoreDbContext ' erişebilir ve kullanabiliriz.
            services.AddScoped<IBookStoreDbContext>(provider=> provider.GetService<BookStoreDbContext>());

            //AutoMapper' servis olarak ekleyeceğiz.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //ILoggerService interface inde kalıtım olan ConsoleLogger service 'inin uygulamamıza tanıttık.ConsoleLogger yerine DataBaseLogger ı uygularsak bu loglama yaptığımız veriler Database e loglanır.Burada Dependency den kurtulduk ve kolayca loglarımızı yönetir hale geldik.
            services.AddSingleton<ILoggerService,Databaselogger>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseAuthentication();  //authentication uygulanmadan authorization kullanılırsa durmandan not found response alınır.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
