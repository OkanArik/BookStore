using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorCommandModel Model { get; set; }

        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(author=> author.Id == AuthorId);

            if(author is null)
              throw new InvalidOperationException("Author mevcut değil.");
            
            author.Name=Model.Name== default? author.Name:Model.Name;
            author.Surname=Model.Surname==default?author.Surname:Model.Surname;

            _dbContext.SaveChanges();

        }
    }
    public class UpdateAuthorCommandModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public DateTime BirthDay { get; set; } //Doğum tarihini güncellemeye kapattım. Bunu sadece burada model üzerinden dışarıdan verileri alıp entitye aktarmanın yararını görmek için yaptım.
    }
}