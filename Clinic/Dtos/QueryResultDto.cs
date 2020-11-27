using System.Collections.Generic;

namespace Clinic.Dtos
{
    public class QueryResultDto<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}