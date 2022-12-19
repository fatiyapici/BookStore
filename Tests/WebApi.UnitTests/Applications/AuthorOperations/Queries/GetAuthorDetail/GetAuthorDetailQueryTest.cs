using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIsNotFound_InvalidOperationException_ShouldReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);

            query.AuthorId = 1;

            FluentActions.
                Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Yazar bulunamadi.");
        }
        
        [Fact]
        public void WhenAuthorIsFound_Author_ShouldReturn()
        {
            var author = new Author()
            {
                Id = 5,
                Name = "Victor",
                Surname = "Hugo",
                Birthday = new DateTime(1802, 02, 26)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = author.Id;

            FluentActions.
                Invoking(() => query.Handle()).Invoke();
        }
    }
}