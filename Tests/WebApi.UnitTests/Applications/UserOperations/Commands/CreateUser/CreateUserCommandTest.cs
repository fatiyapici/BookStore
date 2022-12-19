using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Applications.UserOperations.Commands.CreateUserCommand;

namespace Tests.WebApi.UnitTests.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenUsernameIsExist_InvalidOperationException_ShouldReturn()
        {
            var user = new User()
            {
                Name = "Baran",
                Surname = "Taylan",
                Email = "baran@gmail.com",
                Password = "123456",
                RefreshToken = "",
                RefreshTokenExpireDate = DateTime.Now.AddHours(2)
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = new CreateUserModel()
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Password = user.Password
            };

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kullanici adi zaten mevcut.");
        }
    }
}