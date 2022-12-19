using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenToBeDeletedBookIsNotFound_InvalidOperationException_ShouldReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 5;

            FluentActions.
                Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Silinecek kitap bulunamadi.");
        }
    }
}