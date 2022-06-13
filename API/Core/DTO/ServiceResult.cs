using System.Collections.Generic;

namespace Core.DTO
{
    public class ServiceResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
