using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIsNotFound_InvalidOperationException_ShouldReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);

            query.BookId = 1;

            FluentActions.
                Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Kitap bulunamadi.");
        }

        [Fact]
        public void WhenBookIsFound_Book_ShouldReturn()
        {
            var book = new Book()
            {
                Title = "WhenBookIsFound_Book_ShouldReturn",
                PageCount = 100,
                PublishDate = new DateTime(1996, 05, 17),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = book.Id;

            FluentActions.
                Invoking(() => query.Handle()).Invoke();
        }
    }
}