using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Tests.WebApi.UnitTests.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
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
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int bookId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;

            UpdateBookModel model = new UpdateBookModel();
            model.Title = title;
            model.GenreId = genreId;
            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            var book = new Book()
            {
                Id = 1,
                Title = "WhenValidInputsAreGiven_Book_ShouldBeUpdated",
                GenreId = 1
            };

            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = book.Id;

            command.Model = new UpdateBookModel()
            {
                Title = book.Title,
                GenreId = book.GenreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}