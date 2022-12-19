using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.UpdateUser;
using WebApi.DbOperations;
using Xunit;
using static WebApi.Applications.UserOperations.Commands.UpdateUser.UpdateUserCommand;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateUserCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [InlineData("fati@gmail.com", "123456")]
        [InlineData("baran@gmail.com", "123456")]
        [InlineData("fatigmail.com", "123456")]
        [InlineData("fati@gmail.com", "12345")]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string email, string password)
        {
            UpdateUserCommand command = new UpdateUserCommand(null);

            UpdateUserModel model = new UpdateUserModel();
            model.Email = email;
            model.Password = password;
            command.Model = model;

            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}