// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation;

namespace IT.Test.Application.User.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Surname).NotEmpty();
            RuleFor(u => u.Number).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
        }
    }
}
