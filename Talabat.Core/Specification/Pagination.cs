using System.Collections.Generic;
using System.Threading.Tasks;
using TalabatCore.Specification;

namespace Talabat.Helper
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
        public Pagination(int pageindex,int pagesize,int count, IReadOnlyList<T> data)
        {
            PageIndex= pageindex;
            PageSize=pagesize;
            Count=count;
            Data= data;
        }
    }
}
