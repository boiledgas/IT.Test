// Тестовое задание https://github.com/boiledgas/IT.Test

using FluentValidation;
using IT.Test.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace IT.Test.StorageService.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            switch (context.Exception)
            {
                case ValidationException e:
                    context.Result = new BadRequestObjectResult(e.Errors);
                    break;
                case UserNotFoundException u:
                    context.Result = new NotFoundObjectResult(new { ErrorMessage = $"User not found {u.Email}" });
                    break;
                default:
                    context.ExceptionHandled = false;
                    break;
            }
        }
    }
}
