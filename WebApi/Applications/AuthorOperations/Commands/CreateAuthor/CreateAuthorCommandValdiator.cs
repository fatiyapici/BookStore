using FluentValidation;

namespace WebApi.Applications.AuthorOperations.CreateAuthor
{
    public class CreateAuthorCommandValdiator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValdiator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.Surname).MinimumLength(2);
            RuleFor(commamd => commamd.Model.Birthday).LessThan(DateTime.Now);
        }
    }
}