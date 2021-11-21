// Тестовое задание https://github.com/boiledgas/IT.Test

using System;

namespace IT.Test.Application.Exceptions
{
    public class UserExistException : ApplicationException
    {
        public string Email { get; }
        public UserExistException(string email)
        {
            Email = email;
        }
    }
}
