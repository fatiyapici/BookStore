using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenToBeDeletedGenreIsNotFound_InvalidOperationException_ShouldReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 5;

            FluentActions.
                Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Silinecek tur bulunamadi.");
        }
    }
}