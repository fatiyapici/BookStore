using FluentValidation;

namespace WebApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryValidator : AbstractValidator<GetGenresQuery>
    {
        public GetGenresQueryValidator()
        {
        }
    }
}