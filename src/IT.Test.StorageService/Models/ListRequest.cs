// Тестовое задание https://github.com/boiledgas/IT.Test

using System.ComponentModel;

namespace IT.Test.StorageService.Models
{
    public class ListRequest
    {
        [DefaultValue("Информационные технологии")]
        public string Organization { get; set; }
        [DefaultValue(0)]
        public int Offset { get; set; }
        [DefaultValue(10)]
        public int Count { get; set; }
    }
}
