// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation;
using IT.Test.Api.Models;

namespace IT.Test.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Surname).NotEmpty();
            RuleFor(u => u.Number).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
        }
    }
}
