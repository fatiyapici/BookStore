using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using Xunit;
using static WebApi.Applications.UserOperations.Commands.CreateUserCommand;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [InlineData("fati@gmail.com", "123456", "Fatih", "Yapici")]
        [InlineData("baran@gmail.com", "123456", "B", "Taylan")]
        [InlineData("fatigmail.com", "123456", "Fatih", "Yapici")]
        [InlineData("fati@gmail.com", "12345", "Fatih", "Yapici")]

        [Theory]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string email, string password, string name, string surname)
        {
            CreateUserCommand command = new CreateUserCommand(null, null);
            command.Model = new CreateUserModel()
            {
                Email = email,
                Password = password,
                Name = name,
                Surname = surname
            };

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}