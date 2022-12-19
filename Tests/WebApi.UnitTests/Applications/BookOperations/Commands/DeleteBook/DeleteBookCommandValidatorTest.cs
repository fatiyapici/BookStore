using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 2)]
        [InlineData("Lord Of The Rings", 100, 0, 3)]
        [InlineData("", 0, 0, 4)]
        [InlineData("", 100, 1, 5)]
        [InlineData("", 0, 1, 6)]
        [InlineData("Lor", 100, 1, 7)]
        [InlineData("Lor", 100, 0, 8)]
        [InlineData("Lord", 0, 1, 9)]
        [InlineData(" ", 100, 1, 0)]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int BookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = BookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            var book = new Book()
            {
                Title = "WhenValidInputsAreGiven_Book_ShouldBeDeleted",
                GenreId = 1,
                PageCount = 100
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            command.BookId = book.Id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            result.Errors.Count.Should().Be(0);
        }
    }
}