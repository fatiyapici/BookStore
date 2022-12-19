using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGenreIsNotFound_InvalidOperationException_ShouldReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);

            query.GenreId = 1;

            FluentActions.
                Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Kitap kategorisi bulunamadi.");
        }
        
        [Fact]
        public void WhenBookIsFound_Book_ShouldReturn()
        {
            var genre = new Genre()
            {
                Id = 1,
                Name = "WhenGenreIsFound_Genre_ShouldReturn",
                IsActive = true

            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = genre.Id;

            FluentActions.
                Invoking(() => query.Handle()).Invoke();
        }
    }
}