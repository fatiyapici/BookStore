using System;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.UpdateUser;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Applications.UserOperations.Commands.UpdateUser.UpdateUserCommand;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenUpdatedUserIsNotExist_InvalidOperationException_ShouldReturn()
        {
            var user = new User()
            {
                Name = "Fatih",
                Surname = "Yapici",
                Email = "fati@gmail.com",
                Password = "123456",
                RefreshToken = ""
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context);

            command.Model = new UpdateUserModel()
            {
                Email = "fati@gmail.com",
                Password = "123456"
            };

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Guncellenecek kullanici bulunamadi.");
        }
    }
}