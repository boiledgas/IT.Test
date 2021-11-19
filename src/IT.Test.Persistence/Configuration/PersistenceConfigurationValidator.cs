// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation;

namespace IT.Test.Model.Configuration
{
    public class PersistenceConfigurationValidator : AbstractValidator<PersistenceConfiguration>
    {
        public PersistenceConfigurationValidator()
        {
            RuleFor(x => x.Host).NotEmpty();
            RuleFor(x => x.Port).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.User).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
