using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class PagerDto<TItem>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ItemCount { get; set; }
        public int PageCount { get; set; }

        public List<TItem> Items { get; set; }

        public PagerDto()
        {
            PageNumber = 1;
            PageSize = 50;
            ItemCount = 0;
            PageCount = 0;

            Items = new List<TItem>();
        }
    }
}
