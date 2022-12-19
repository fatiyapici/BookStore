using FluentValidation;

namespace WebApi.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {

        }
    }
}