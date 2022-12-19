using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenToBeUpdatedBookIsNotFound_InvalidOperationException_ShouldReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Guncellenecek kitap bulunamadi.");
        }
    }
}