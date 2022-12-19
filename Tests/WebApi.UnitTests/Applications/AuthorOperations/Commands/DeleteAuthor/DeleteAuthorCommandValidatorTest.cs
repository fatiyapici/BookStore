using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [InlineData("William", "Shakespeare", 1564, 04, 26, 0)]
        [InlineData("Jane", "Austen", 1775, 12, 16, 1)]
        [InlineData("Charles", "Dickens", 1812, 02, 07, 2)]
        [InlineData("Leo", "Tolstoy", 1828, 09, 09, 3)]
        [InlineData("Ernest", "Hemingway", 1899, 07, 21, 4)]
        [InlineData("Franz", "Kafka", 1883, 07, 03, 5)]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, int year, int month, int day, int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
             var author = new Author()
            {
                Id = 5,
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            command.AuthorId = author.Id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            result.Errors.Count.Should().Be(0);
        }
    }
}