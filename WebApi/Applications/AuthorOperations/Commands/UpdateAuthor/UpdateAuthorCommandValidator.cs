using FluentValidation;
using WebApi.Applications.AuthorOperations.UpdateAuthors;

namespace WebApi.Applications.AuthorOperations.DeleteAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname.Trim() != string.Empty);
            RuleFor(command => command.Model.Birthday).LessThan(DateTime.Now);
        }
    }
}