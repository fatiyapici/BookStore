using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [InlineData("Fantasy", true, 1)]
        [InlineData("Adventure", true, 2)]
        [InlineData("Travel", false, 3)]
        [InlineData("Mystery", false, 4)]
        [InlineData("Art", true, 5)]
        [InlineData("War", true, 0)]

        [Theory]
        public void WhenInvalidGenreInputAreGiven_Validator_ShouldBeReturnErrors(string name, bool isActive, int genreId)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;

            UpdateGenreModel model = new UpdateGenreModel();
            model.Name = name;
            model.IsActive = isActive;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            var genre = new Genre()
            {
                Id = 1,
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeUpdated",
                IsActive = true
            };

            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genre.Id;

            command.Model = new UpdateGenreModel()
            {
                Name = genre.Name,
                IsActive = genre.IsActive
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}