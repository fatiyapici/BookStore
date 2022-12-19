using FluentValidation;

namespace WebApi.Applications.AuthorOperations.GetAuthors
{
    public class GetAuthorsQueryValdiator : AbstractValidator<GetAuthorsQuery>
    {
        public GetAuthorsQueryValdiator()
        {
        }
    }
}