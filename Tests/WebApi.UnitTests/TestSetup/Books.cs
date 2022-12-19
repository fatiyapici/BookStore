using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book { AuthorId = 1, Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
            new Book { AuthorId = 2, Title = "Herland", GenreId = 2, PageCount = 300, PublishDate = new DateTime(2010, 05, 23) },
            new Book { AuthorId = 3, Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) }
            );
        }
    }
}