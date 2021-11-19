// Тестовое задание https://github.com/boiledgas/IT.Test

using System.Collections.Generic;

namespace IT.Test.StorageService.Models
{
    public class PaginationResponse<T>
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
