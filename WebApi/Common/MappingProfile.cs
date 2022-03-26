using AutoMapper;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.USerOperations.Commands.CreateUSer;
using WebApi.Entities;
using static WebApi.Applications.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile // Profile class'ından kalıtım alarak AutoMapper tarafından config dosyası olarak görülmesi sağlandı.
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();//CreateBookModel objesi Book objesine map lenebilir dedim.
            CreateMap<Book,GetBookDetailViewModel>()
                                                   .ForMember(destination=> destination.Genre,option=> option.MapFrom(source=> (source.Genre.Name)))
                                                   .ForMember(destination=> destination.PublishDate,option=> option.MapFrom(source=> source.PublishDate.Date.ToString("dd-MM-yyyy"))).ForMember(destination=> destination.Author,option=>option.MapFrom(source=>(source.Author.Name)));
            CreateMap<Book,GetBooksViewModel>().ForMember(destination=> destination.Genre,option=> option.MapFrom(source=> (source.Genre.Name)))
                                                   .ForMember(destination=> destination.PublishDate,option=> option.MapFrom(source=> source.PublishDate.Date.ToString("dd-MM-yyyy"))).ForMember(destination=> destination.Author,option=>option.MapFrom(source=>(source.Author.Name)));

            
            CreateMap<Genre,GetGenresViewModel>();
            CreateMap<Genre,GetGenreDetailViewModel>();

            CreateMap<Author,GetAuthorsQueryViewModel>().ForMember(dst=> dst.BirthDay,opt=> opt.MapFrom(src=> src.BirthDay.Date.ToString("dd-MM-yyyy")));
            CreateMap<Author,GetAuthorDetailQueryViewModel>().ForMember(dst=> dst.BirthDay,opt=> opt.MapFrom(src=> src.BirthDay.Date.ToString("dd-MM-yyyy")));
            CreateMap<CreateAuthorCommandModel,Author>();

            CreateMap<CreateUserModel,User>();
        }
    }
}