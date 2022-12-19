using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.CreateAuthor;
using WebApi.DbOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [InlineData("William", "Shakespeare", 1564, 04, 26)]
        [InlineData("Jane", "Austen", 1775, 12, 16)]
        [InlineData("Charles", "Dickens", 1812, 02, 07)]
        [InlineData("Leo", "Tolstoy", 2028, 09, 09)]
        [InlineData("Ernest", "He", 1899, 07, 21)]
        [InlineData("", "Kafka", 1883, 07, 03)]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(string name, string surname, int year, int month, int day)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                Birthday = new DateTime(year, month, day)
            };

            CreateAuthorCommandValdiator validator = new CreateAuthorCommandValdiator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(2030, 02, 26),
            };

            CreateAuthorCommandValdiator validator = new CreateAuthorCommandValdiator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26),
            };

            CreateAuthorCommandValdiator validator = new CreateAuthorCommandValdiator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26),
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.Birthday.Should().Be(model.Birthday);
        }
    }
}