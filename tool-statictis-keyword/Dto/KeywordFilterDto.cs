using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class KeywordFilterDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
