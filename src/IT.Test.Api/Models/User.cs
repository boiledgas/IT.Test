// Тестовое задание https://github.com/boiledgas/IT.Test

using System.ComponentModel;

namespace IT.Test.Api.Models
{
    public class User
    {
        [DefaultValue("Name")]
        public string Name { get; set; }
        [DefaultValue("Surname")]
        public string Surname { get; set; }
        [DefaultValue("")]
        public string Patronymic { get; set; }
        [DefaultValue("Number")]
        public string Number { get; set; }
        [DefaultValue("testemail@mail.ru")]
        public string Email { get; set; }
    }
}
