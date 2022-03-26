using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors 
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}