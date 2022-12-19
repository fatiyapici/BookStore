using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.UpdateAuthors;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenToBeUpdatedAuthorIsNotFound_InvalidOperationException_ShouldReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Guncellenecek yazar bulunamadi.");
        }
        
        [Fact]
        public void WhenToBeUpdatedAuthorAlreadyExist_InvalidOperationException_ShouldReturn()
        {
            var author = new Author()
            {
                Id = 5,
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 5;
            command.Model = new UpdateAuthorModel()
            {
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Ayni bilgilere sahip yazar mevcut.");
        }
    }
}