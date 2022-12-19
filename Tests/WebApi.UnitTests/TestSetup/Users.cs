using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Users
    {
        public static void AddUser(this BookStoreDbContext context)
        {
            context.Users.AddRange(
                new User {
                    Name = "Fatih",
                    Surname = "Yapici",
                    Email = "fati@gmail.com",
                    Password = "123456",
                    RefreshToken = "",
                    RefreshTokenExpireDate = System.DateTime.Now.AddHours(2)
                });
        }
    }
}