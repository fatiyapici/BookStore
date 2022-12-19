using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenToBeDeletedAuthorIsNotFound_InvalidOperationException_ShouldReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 5;

            FluentActions.
                Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Silinecek yazar bulunamadi.");
        }

        [Fact]
        public void WhenToBeDeletedAuthorHasBook_InvalidOperationException_ShouldReturn()
        {
            var author = new Author()
            {
                Id = 5,
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };
            _context.Authors.Add(author);

            var book = new Book()
            {
                AuthorId = 5,
                Title = "Sefiller",
                GenreId = 3,
                PageCount = 100,
                PublishDate = new DateTime(1962, 01, 01)
            };
            _context.Books.Add(book);

            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 5;

            FluentActions.
                Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Bu yazarin kitabi mevcut.");
        }
    }
}