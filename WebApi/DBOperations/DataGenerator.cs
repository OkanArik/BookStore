using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator //İnitial olarak database de veri oması için oluşturdum bu class'ı.
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))//scope içerisinde kullanmak için context yarattık.
            {
                if ( context.Books.Any())//DB de veri varsa true olur
                {
                    return;// Scope dan çıkar
                }

                context.Books.AddRange(
                    new Book{
                        Title="Lean Startup",
                        GenreId=1,//Personal Growth
                        PageCount=200,
                        PublishDate=new DateTime(2001,06,12),
                        AuthorId=1
                    },
                    new Book{
                        Title="Herland",
                        GenreId=2,//Science Fiction
                        PageCount=250,
                        PublishDate=new DateTime(2010,05,23),
                        AuthorId=2
                    },
                    new Book{
                        Title="Dune",
                        GenreId=2,//Science Fiction
                        PageCount=540,
                        PublishDate=new DateTime(2001,12,21),
                        AuthorId=3
                    }
                );

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth",
                    },
                    new Genre{
                        Name = "Science Fiction",
                    },
                    new Genre{
                        Name = "Romance",
                    }
                );

                context.Authors.AddRange(
                    new Author{
                        Name = "Eric",
                        Surname = "Ries",
                        BirthDay = new DateTime(1978,09,22)
                    },
                    new Author{
                        Name = "Charlotte",
                        Surname = "Perkins Gilman",
                        BirthDay = new DateTime(1860,07,03)
                    },
                    new Author{
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDay = new DateTime(1920,10,08)
                    }
                );

                context.SaveChanges();//Entity üzerinde bir değişiklik yapıldığında SaveChanges() methodu ile değişiklikler DB ye kaydedilmelidir.
            }
        }
    }
}