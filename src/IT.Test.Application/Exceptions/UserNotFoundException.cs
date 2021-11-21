// Тестовое задание https://github.com/boiledgas/IT.Test

using System;

namespace IT.Test.Application.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public string Email { get; }
        public UserNotFoundException(string email)
        {
            Email = email;
        }
    }
}
