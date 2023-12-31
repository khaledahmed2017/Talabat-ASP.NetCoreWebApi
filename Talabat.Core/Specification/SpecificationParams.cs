using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatCore.Specification
{
    public class SpecificationParams
    {
        public string sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private int pageSize=5;
        
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        int MaxPageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize=value>MaxPageSize?MaxPageSize:value; }
        }

        public int PageIndex { get; set; } = 1;

    }
}
