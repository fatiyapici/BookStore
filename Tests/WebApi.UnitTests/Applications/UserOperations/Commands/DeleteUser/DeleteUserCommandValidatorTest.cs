using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.DeleteUser;
using WebApi.DbOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteUserCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [InlineData("fati@gmail.com", "123456")]
        [InlineData("baran@gmail.com", "123456")]
        [InlineData("fatigmail.com", "123456")]
        [InlineData("fati@gmail.com", "12345")]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string email, string password)
        {
            DeleteUserCommand command = new DeleteUserCommand(null, null);
            command.Model = new DeleteUserModel()
            {
                Email = email,
                Password = password,
            };

            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}