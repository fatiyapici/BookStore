using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Applications.UserOperations.Commands.CreateToken.CreateTokenCommand;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }
        
        [Fact]
        public void WhenUsernameAndPasswordWrong_InvalidOperationException_ShouldReturn()
        {
            var user = new User()
            {
                Name = "Fatih",
                Surname = "Yapici",
                Email = "fati@gmail.com",
                Password = "123456",
                RefreshToken = "",
                RefreshTokenExpireDate = DateTime.Now.AddHours(2)
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = new CreateTokenModel()
            {
                Email = "test",
                Password = "test"
            };

            FluentActions.
            Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kullanici adi-sifre hatali.");
        }
    }
}