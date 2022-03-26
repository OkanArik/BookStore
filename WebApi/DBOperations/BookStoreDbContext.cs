using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext :DbContext ,IBookStoreDbContext//DbContext'ten kalıtım alır.
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base (options){}

        public DbSet<Book> Books {get; set;} //Bu satır ile bu context'e Book entity sini ekledim ve Books ismiyle bu entity nin herşeyine erişebilirim.Database deki obje ile koddaki nesne arasında bir köprü kurdum.

        public DbSet<Genre> Genres {get; set;}

        public DbSet<Author> Authors{get; set;}
        public DbSet<User> Users{get; set;}

        public override int SaveChanges()
        {
            return base.SaveChanges() ;
        }
    }
}