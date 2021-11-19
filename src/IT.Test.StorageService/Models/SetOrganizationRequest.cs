// Тестовое задание https://github.com/boiledgas/IT.Test

using System.ComponentModel;

namespace IT.Test.StorageService.Models
{
    public class SetOrganizationRequest
    {
        [DefaultValue("Информационные технологии")]
        public string OrganizationName { get; set; }
        [DefaultValue("testemail@mail.ru")]
        public string Email { get; set; }
    }
}
