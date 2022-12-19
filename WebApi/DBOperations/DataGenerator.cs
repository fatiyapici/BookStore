using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                   new Book
                   {
                       // Id = 1,
                       Title = "Lean Startup",
                       GenreId = 1,
                       AuthorId = 1,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                       // Id = 2,
                       Title = "Herland",
                       AuthorId = 2,
                       GenreId = 2,
                       PageCount = 300,
                       PublishDate = new DateTime(2010, 05, 23)
                   }, new Book
                   {
                       // Id = 3,
                       Title = "Dune",
                       AuthorId = 3,
                       GenreId = 2,
                       PageCount = 540,
                       PublishDate = new DateTime(2001, 12, 21)
                   }
               );

                context.Genres.AddRange(
                     new Genre
                     {
                         Name = "Personal Growth",
                         IsActive = true

                     },
                     new Genre
                     {
                         Name = "Science Fiction",
                         IsActive = true
                     },
                     new Genre
                     {
                         Name = "Romance",
                         IsActive = true
                     }
                     );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        Birthday = new DateTime(1978, 09, 22)

                    },
                    new Author
                    {
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        Birthday = new DateTime(1935, 08, 17)

                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Habert",
                        Birthday = new DateTime(1920, 10, 8)

                    });

                context.Users.AddRange(
                    new User
                    {
                        Name = "Fatih",
                        Surname = "Yapici",
                        Email = "fati@gmail.com",
                        Password = "123456",
                        RefreshToken = "",
                        RefreshTokenExpireDate = DateTime.Now.AddHours(2)
                    });

                context.SaveChanges();
            }
        }
    }
}