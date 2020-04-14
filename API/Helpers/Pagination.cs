using System.Collections.Generic;
using API.Dtos;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {

        public Pagination(IReadOnlyList<T> data, int count, int pageSize, int pageIndex)
        {
            Data = data;
            Count = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }
    }
}