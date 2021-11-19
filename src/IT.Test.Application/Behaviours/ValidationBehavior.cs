// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace IT.Test.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var validationResults = new List<ValidationResult>(1);
                foreach (IValidator<TRequest> validator in _validators)
                    validationResults.Add(await validator.ValidateAsync(request, cancellationToken));

                var failures = validationResults
                    .Where(result => !result.IsValid)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
