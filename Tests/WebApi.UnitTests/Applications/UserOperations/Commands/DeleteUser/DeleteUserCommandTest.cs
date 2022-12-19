using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.DeleteUser;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteUserCommandTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenDeletedUserIsNotExist_InvalidOperationException_ShouldReturn()
        {
            var user = new User()
            {
                Name = "Burak",
                Surname = "Hassemercioglu",
                Email = "burak@gmail.com",
                Password = "123456",
                RefreshToken = "",
                RefreshTokenExpireDate = DateTime.Now.AddHours(2)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            DeleteUserCommand command = new DeleteUserCommand(_context, _mapper);
            command.Model = new DeleteUserModel()
            {
                Email = "bura@gmail.com",
                Password = "123456"
            };

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Silinecek kullanici bulunamadi.");
        }
    }
}