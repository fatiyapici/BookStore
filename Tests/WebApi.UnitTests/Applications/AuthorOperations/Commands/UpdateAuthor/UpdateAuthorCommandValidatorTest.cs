using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.DeleteAuthor;
using WebApi.Applications.AuthorOperations.UpdateAuthors;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [InlineData("William", "Shakespeare", 1564, 04, 26, 0)]
        [InlineData("Jane", "Austen", 1775, 12, 16, 1)]
        [InlineData("Charles", "Dickens", 1812, 02, 07, 2)]
        [InlineData("Leo", "Tolstoy", 2050, 09, 09, 3)]
        [InlineData("Ernest", "Hemingway", 2040, 07, 21, 4)]
        [InlineData("Franz", "Kafka", 2030, 07, 03, 5)]

        [Theory]
        public void WhenInvalidGenreInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, int year, int month, int day, int id)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = id;

            UpdateAuthorModel model = new UpdateAuthorModel();
            model.Name = name;
            model.Surname = surname;
            model.Birthday = new DateTime(year, month, day);
            command.Model = model;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            var author = new Author()
            {
                Id = 1                                                                         ,
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = author.Id;

            command.Model = new UpdateAuthorModel()
            {
                Name = author.Name,
                Surname = author.Surname,
                Birthday = author.Birthday
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}