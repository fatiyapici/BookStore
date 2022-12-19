using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
            new Author { Name = "Eric", Surname = "Ries", Birthday = new DateTime(1978, 09, 22) },
            new Author { Name = "Charlotte Perkins", Surname = "Gilman", Birthday = new DateTime(1935, 08, 17) },
            new Author { Name = "Frank", Surname = "Habert", Birthday = new DateTime(1920, 10, 8) }
            );
        }
    }
}