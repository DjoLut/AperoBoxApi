using System;
using System.Collections.Generic;
using System.Text;

namespace AperoBoxApi.DTO
{
    public class PagingResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}